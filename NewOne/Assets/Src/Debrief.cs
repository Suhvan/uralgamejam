using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debrief : MonoBehaviour {

	[SerializeField]
	Text debrief;


	// Use this for initialization
	void Start () {
		debrief.text = "You killed " + GameCore.Instance.Score + " tanks";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onRestart()
	{
		Time.timeScale = 1; 
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
