using UnityEngine;
using System.Collections;

public class Mortal : MonoBehaviour {

	[SerializeField]
	int _hp = 1;

	public int HP
	{
		get
		{
			return _hp;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GetDamage(int dmg)
	{
		_hp -= dmg;
		if (_hp <= 0)
		{
			onDeath();
		}
	}

	protected virtual void onDeath()
	{

	}
}
