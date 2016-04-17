using UnityEngine;
using System.Collections;

public class Tank : Mortal
{

	[SerializeField]
	private Module fireModule;

	[SerializeField]
	private ShaftModule shaftModule;

	[SerializeField]
	private Transform shootingPoint;

    [SerializeField]
    private Animator gunAnim;


	protected override void onDeath()
	{
		base.onDeath();
		GameCore.Instance.GameOver();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireModule!=null && fireModule.Triggered)
		{
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot()
	{
		fireModule.ReleaseTrigger();
		var bulletPref = shaftModule.GetBullet();
		if (bulletPref != null)
		{
			if (gunAnim)
				gunAnim.SetTrigger("shoot");
			yield return new WaitForSeconds(0.3f);
			var bullet = (Projectile)Instantiate(bulletPref, shootingPoint.position, shootingPoint.rotation);
			bullet.Kickstart(0);
		}
		else
		{
			//TopPanel.Instance.setStatus("Нет патронов, чтобы стрелять");
		}
		yield return null;
	}



}
