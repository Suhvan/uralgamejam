using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool Active { get; set; }

	private bool m_pressed;
	public bool Pressed
	{
		get
		{
			return m_pressed && Active;
		}
	}


	public void Press()
	{
		if (!Pressed)
			m_pressed = true;
	}

	public void Release()
	{
		if (Pressed)
			m_pressed = false;
	}

	
}
