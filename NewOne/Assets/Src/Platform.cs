using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	[SerializeField]
	Hero player;

	[SerializeField]
	Collider2D collider;
	
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		onChange(player.getYPos() < transform.position.y);
	}

	protected virtual void onChange(bool change)
	{
		collider.isTrigger = change;
	}

}
