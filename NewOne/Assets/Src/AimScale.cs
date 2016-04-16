using UnityEngine;
using System.Collections;

public class AimScale : MonoBehaviour {

	[SerializeField]
	Gun Cannon;

	[SerializeField]
	GameObject leftPlatform;

	[SerializeField]
	GameObject rightPlatform;

	[SerializeField]
	Button leftButton;

	[SerializeField]
	Button rightButton;

	[SerializeField]
	float changeSpeed = 0.02f;

	[SerializeField]
	Transform top;

	[SerializeField]
	Transform bottom;
	


	float yRange;

	// Use this for initialization
	void Start() {
		yRange = top.position.y - bottom.position.y;
		if (yRange == 0)
		{
			yRange = 1;
		}

		leftPlatform.transform.position = new Vector3(leftPlatform.transform.position.x, top.position.y, leftPlatform.transform.position.z);
		rightPlatform.transform.position = new Vector3(rightPlatform.transform.position.x, bottom.position.y, rightPlatform.transform.position.z);

		Cannon.Angle = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (leftButton.Pressed)
		{
			DoScaling(leftPlatform, rightPlatform);
		}
		else if (rightButton.Pressed)
		{
			DoScaling(rightPlatform, leftPlatform);
		}
	}


	void DoScaling(GameObject downer, GameObject upper)
	{
		if (downer.transform.position.y - changeSpeed < bottom.position.y || upper.transform.position.y + changeSpeed > top.position.y)
			return;
		downer.transform.position = downer.transform.position + new Vector3(0, -changeSpeed, 0);
		upper.transform.position = upper.transform.position + new Vector3(0, changeSpeed, 0);
		AdjustAngle();
	}

	void AdjustAngle()
	{
		var value = rightPlatform.transform.position.y - bottom.position.y;
		Cannon.Angle = (value / yRange) * 180;
	}

	
}
