using UnityEngine;
using System.Collections;

public class EnemyTank : BasicShooter
{

    [SerializeField]
    float actionInterval = 3.0f;
    float actionTimer = 0.0f;

    [SerializeField]
    Transform aimPoint;

    [SerializeField]
    Gun gunToAim;

    [SerializeField]
    float idealDistance = 20.0f;

    //
    bool lastActionWasReposition = false;

    [SerializeField]
    float moveSpeed = 2.0f;
    [SerializeField]
    float moveDuration = 2.0f;

    float moveDirection = -1.0f;
    float moveTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        // small offset
        actionTimer -= Random.Range(0.1f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer > actionInterval)
        {
            actionTimer -= actionInterval;
            DoAction();
        }

        // update movement
        if (moveTimer > 0.0f)
        {
            moveTimer -= Time.deltaTime;
            Vector3 newPos = gameObject.transform.position;
            newPos.x += moveDirection * moveSpeed * Time.deltaTime;
            gameObject.transform.position = newPos;
        }
    }

    //
    void DoAction()
    {
        Debug.Log("DoAction");
        // pick action first
        // don't move twice
        if (lastActionWasReposition)
        {
            DoShooting();
        }
        else
        {
            if (Random.value > 0.5f)
                DoShooting();
            else
                DoReposition();
        }

    }


    //
    void DoReposition()
    {
        lastActionWasReposition = true;

        bool moveLeft = idealDistance < gameObject.transform.position.x - aimPoint.position.x;
        // move 'wrong' way sometimes
        if (Random.value > 0.8f)
            moveLeft = !moveLeft;

        moveDirection = moveLeft ? -1.0f : 1.0f;
        moveTimer = moveDuration;
    }

    //
    void DoShooting()
    {
        lastActionWasReposition = false;

        // aim vaguely
        float initialForce = Random.Range( -3, 3 );
        float initialVelocity = (projPrefab.shotForce + initialForce) / projPrefab.GetComponent<Rigidbody2D>().mass;

        float approxAlpha = Mathf.Asin( (shootingPoint.position.x - aimPoint.position.x) * projPrefab.GetComponent<Rigidbody2D>().gravityScale * 10 / (initialVelocity * initialVelocity) ) / 2;

        // miss much!
        approxAlpha *= Random.Range(0.8f, 1.8f);
        Debug.Log("approxAlpha " + approxAlpha);
        gunToAim.Angle = 180 - Mathf.Rad2Deg * approxAlpha;


        //
        Shoot(initialForce);
    }
}
