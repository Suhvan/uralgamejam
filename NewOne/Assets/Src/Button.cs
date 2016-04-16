using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Press()
	{
		if (!Pressed)
			Pressed = true;
	}

	public void Release()
	{
		if (Pressed)
			Pressed = false;
	}


	public bool Pressed { get; private set; }
}
