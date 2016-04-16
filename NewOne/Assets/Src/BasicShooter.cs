using UnityEngine;
using System.Collections;

public class BasicShooter : Mortal {
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


	protected override void onDeath()
	{
		base.onDeath();
		Destroy(gameObject);
		GameCore.Instance.Score++;
	}
}
