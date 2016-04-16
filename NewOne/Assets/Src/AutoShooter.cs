using UnityEngine;
using System.Collections;

public class AutoShooter : BasicShooter
{

    [SerializeField]
    float shotInterval = 3.0f;

    float shootTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        // small offset
        shootTimer += Random.Range(0.1f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shotInterval)
        {
            shootTimer -= shotInterval;
            Shoot(Random.Range(0, 20));
        }
    }
}
