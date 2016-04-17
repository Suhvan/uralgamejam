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

    [SerializeField]
    float engagementDistance = 120.0f;

    //
    bool lastActionWasReposition = false;

    [SerializeField]
    float moveSpeed = 2.0f;
    [SerializeField]
    float moveDuration = 2.0f;

    [SerializeField]
    float precision = 0.8f;

    float moveDirection = -1.0f;
    float moveTimer = 0.0f;

    //
    bool moveToEngage = true;

    // Use this for initialization
    void Start()
    {
		// small offset
		aimPoint = GameCore.Instance.mainTank.transform;
        actionTimer -= Random.Range(0.1f, 0.9f) * actionInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToEngage)
        {
            // we are to the right
            bool moveLeft = gameObject.transform.position.x > aimPoint.position.x;
            moveDirection = moveLeft ? -1.0f : 1.0f;
            moveTimer = moveDuration;
        }
        else
        {
            actionTimer += Time.deltaTime;
            if (actionTimer > actionInterval)
            {
                actionTimer -= actionInterval;
                DoAction();
            }
        }

        //
        moveToEngage = Mathf.Abs(aimPoint.transform.position.x - gameObject.transform.position.x) > engagementDistance;

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

        Vector3 aimPointMod = aimPoint.transform.position;
        // miss much!
        bool miss = Random.value > precision;
        if( miss )
        {
            if (Random.value > 0.5)
                aimPointMod.x += Random.Range(8.0f, 18.0f);
            else
                aimPointMod.x -= Random.Range(12.0f, 25.0f);
        }

        // aim vaguely
        float initialForce = Random.Range( -3, 3 );
        float initialVelocity = (projPrefab.shotForce + initialForce) / projPrefab.GetComponent<Rigidbody2D>().mass;

        // ignore vertical difference
        float approxAlpha = Mathf.PI/4;
        float alphaSin = Mathf.Abs(shootingPoint.position.x - aimPointMod.x) * projPrefab.GetComponent<Rigidbody2D>().gravityScale * Mathf.Abs(Physics2D.gravity.y) / (initialVelocity * initialVelocity);
        if( Mathf.Abs( alphaSin ) >= 0.999 )
        {
            // no solution, sorry
            return;
        }
        else
        {
            approxAlpha = Mathf.Asin( alphaSin ) / 2;
        }

        bool shootLeft = shootingPoint.position.x > aimPointMod.x;
        
        approxAlpha = Mathf.Clamp( approxAlpha, 0, Mathf.PI / 4 );

        // there also could be 2 solutions: "low" or "hi" angle, Asin provides us with "low" one
        // make it "hi" 75% of the time
        // also always hi when missing
        if (Random.value <= 0.75f || miss)
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
