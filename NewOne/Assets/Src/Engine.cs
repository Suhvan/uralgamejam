using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	[SerializeField]
	Button leftButton;

	[SerializeField]
	Button rightButton;

	[SerializeField]
	GameObject tank;

	[SerializeField]
	float moveSpeed = 1f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (leftButton.Pressed)
		{
			moveTank(-1);
		}
		else if (rightButton.Pressed)
		{
			moveTank(1);
		}
	}

	void moveTank(int moveDirection)
	{

		Vector3 newPos = tank.transform.position;
		newPos.x += moveDirection * moveSpeed * Time.deltaTime;
		tank.transform.position = newPos;

	}


}
