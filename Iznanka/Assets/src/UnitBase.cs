using UnityEngine;
using System.Collections;

public class UnitBase : MonoBehaviour {

	public enum UnitAnimState
	{
		IDLE = 0,
		WALK = 1
	}

	[SerializeField]
	protected float maxHp = 100;

	[SerializeField]
	protected float attack = 100;

	[SerializeField]
	protected float speed = 1;

	[SerializeField]
	protected float attackCooldown = 5.0f;

	TextMesh hpText;

	Animator unitAnim;
	protected UnitBase target;

	protected float curHp;
	protected float currentAttackTimer = 0f;

	public int Score { get { return (int)maxHp; } }


	UnitAnimState animState
	{
		set
		{
			if (unitAnim == null)
				return;

			if ((UnitAnimState)unitAnim.GetInteger("State")!= value)
				unitAnim.SetInteger("State", (int)value);
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		curHp = maxHp;
		unitAnim = GetComponent<Animator>();
		hpText = GetComponentInChildren<TextMesh>();
		 
	}

	// Update is called once per frame
	protected virtual void Update () {

		if (hpText != null)
		{
			hpText.text = curHp + "/" + maxHp;
		}

		if (target!=null)
		{
			currentAttackTimer += Time.deltaTime;
			if (currentAttackTimer > attackCooldown)
			{
				currentAttackTimer = 0;
				HitTarget();
			}
        }
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		var otherUnit = other.gameObject.GetComponent<UnitBase>();
        if (otherUnit != null)
		{
			target = otherUnit;
		}
	}

	protected virtual bool HitTarget()
	{		
		if(unitAnim !=null)
			unitAnim.SetTrigger("attack");

		return target.GetHit(attack);
	}

	public virtual bool GetHit(float dmg)
	{
		curHp -= dmg;
		if (curHp <= 0)
		{
			Destroy(gameObject);
			return true;
		}
		return false;
	}

	protected void Walk()
	{
		if (target != null)
		{
			animState = UnitAnimState.IDLE;
			return;
		}
		
		animState = UnitAnimState.WALK;

		transform.position = transform.position + new Vector3(Time.deltaTime * speed, 0, 0);
		
	}
}
