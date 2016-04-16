using UnityEngine;
using System.Collections;

public class EnemyTank : BasicShooter
{

    [SerializeField]
    float actionInterval = 3.0f;
    float actionTimer = 0.0f;

    //[SerializeField]
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
		aimPoint = GameCore.Instance.mainTank.transform;
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

        // we are to the right and too far so
        bool moveLeft = 
            gameObject.transform.position.x > aimPoint.position.x &&
            idealDistance < gameObject.transform.position.x - aimPoint.position.x;

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

        // ignore vertical difference
        float approxAlpha = Mathf.PI/4;
        float alphaSin = Mathf.Abs(shootingPoint.position.x - aimPoint.position.x) * projPrefab.GetComponent<Rigidbody2D>().gravityScale * Mathf.Abs( Physics2D.gravity.y ) / (initialVelocity * initialVelocity);
        if( Mathf.Abs( alphaSin ) >= 0.999 )
        {
            // no solution, sorry, just shoot as far as you can
        }
        else
        {
            approxAlpha = Mathf.Asin( alphaSin ) / 2;
        }

        bool shootLeft = shootingPoint.position.x > aimPoint.position.x;

        // miss much!
        approxAlpha += Random.Range(0.1f, 0.1f);

        // there also could be 2 solutions: "low" or "hi" angle, Asin provides us with "low" one
        // make it "hi" 75% of the time
        if (approxAlpha < Mathf.PI / 4 && Random.value <= 0.75f)
            approxAlpha = ( Mathf.PI / 2 - approxAlpha );

        if (shootLeft)
        {
            gunToAim.Angle = 180 - Mathf.Rad2Deg * approxAlpha;
        }
        else
        {
            gunToAim.Angle = Mathf.Rad2Deg * approxAlpha;
        }

        //
        Shoot(initialForce);
    }
}
