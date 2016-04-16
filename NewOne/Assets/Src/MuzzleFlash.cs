using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}


    // die when animation ends
    public void Suicide()
    {
        Destroy(gameObject);
    }
}
