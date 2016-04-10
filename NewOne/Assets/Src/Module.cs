using UnityEngine;
using System.Collections;

public class Module : MonoBehaviour {

	public bool Triggered { get; protected set; }

	// Use this for initialization
	protected virtual void Start () {
	
	}

	public void ReleaseTrigger()
	{
		Triggered = false;
	}

	public void SetTrigger()
	{
		Triggered = true;
		Debug.Log(gameObject.name +  " pressed");
	}

	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
