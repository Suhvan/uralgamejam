using UnityEngine;
using System.Collections;

public class Knight : UnitBase
{
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		currentAttackTimer = attackCooldown;
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update();		
		Walk();
	}

	protected override bool HitTarget()
	{
		var res = base.HitTarget();
		if (res)
		{
			GameCore.Instance.funScore += target.Score;
			target = null;
			currentAttackTimer = attackCooldown;
			
        }
		return false;
	}

	public override bool GetHit(float dmg)
	{
		var res = base.GetHit(dmg);
		if(res)
			GameCore.Instance.RespawnPlayer();
		return res;
	}


	protected override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		var otherUnit = other.gameObject.GetComponent<Princess>();
		if (otherUnit != null)
		{
			GameCore.Instance.Victory();			
		}
	}



}
