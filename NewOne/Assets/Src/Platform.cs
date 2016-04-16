using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	[SerializeField]
	Collider2D collider;
	
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		onChange(GameCore.Instance.Player.getYPos() < transform.position.y);
	}

	protected virtual void onChange(bool change)
	{
		collider.isTrigger = change;
	}

}
