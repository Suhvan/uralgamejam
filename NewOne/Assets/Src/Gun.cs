using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	private float angle = 0;

	public float Angle 
	{
		set
		{ 
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, value);
			angle = value;
		}
		get
		{
			return angle;
		}

		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
