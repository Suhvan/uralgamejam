using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour {

	[SerializeField]
	Text status; 

	public static TopPanel Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public void setStatus(string msg)
	{
		status.text = msg;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
