using UnityEngine;
using System.Collections;

public class BasicShooter : MonoBehaviour {
    [SerializeField]
    private Module fireModule;

    [SerializeField]
    Projectile projPrefab;

    [SerializeField]
    private Transform shootingPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fireModule.Triggered)
        {
            fireModule.ReleaseTrigger();

            var bullet = (Projectile)Instantiate(projPrefab, shootingPoint.position, shootingPoint.rotation);
            bullet.Kickstart(0);
        }
	}
}
