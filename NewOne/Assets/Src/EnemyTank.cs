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
        Debug.Log(moveLeft);
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

        //
        Shoot(Random.Range( -3, 3 ));
    }
}
