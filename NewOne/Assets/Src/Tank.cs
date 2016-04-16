using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

	[SerializeField]
	private Module fireModule;

	[SerializeField]
	private ShaftModule shaftModule;

	[SerializeField]
	private Transform shootingPoint;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireModule!=null && fireModule.Triggered)
		{
			fireModule.ReleaseTrigger();
			var bulletPref = shaftModule.GetBullet();
			if (bulletPref != null)
			{
				var bullet = (Projectile)Instantiate(bulletPref, shootingPoint.position, Quaternion.identity);
				bullet.Kickstart(0);
			}
			else
			{
				TopPanel.Instance.setStatus("Нет патронов, чтобы стрелять");				
			}
			

			
		}
	}
}
