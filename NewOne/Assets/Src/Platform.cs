using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	[SerializeField]
	Hero player;

	[SerializeField]
	Collider2D collider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		collider.isTrigger = player.getYPos() < transform.position.y;
	}
}
