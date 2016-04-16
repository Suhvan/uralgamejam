using UnityEngine;
using System.Collections;

public class BasicShooter : MonoBehaviour {
    [SerializeField]
    private Module fireModule;

    [SerializeField]
    protected Projectile projPrefab;

    [SerializeField]
    protected Transform shootingPoint;

	// Use this for initialization
	void Start () {
	
	}

    //
    public void Shoot( float power ) {
        var bullet = (Projectile)Instantiate(projPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.Kickstart(power);
        Debug.Log(bullet.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (fireModule && fireModule.Triggered)
        {
            fireModule.ReleaseTrigger();

            Shoot( 0 );
        }
	}
}
