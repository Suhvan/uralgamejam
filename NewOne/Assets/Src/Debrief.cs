using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debrief : MonoBehaviour {

	[SerializeField]
	Text debrief;


	// Use this for initialization
	void Start () {
		//BEST OPTOMIZATION
		if (GameCore.Instance.Score != 1)
		{
			debrief.text = "YOU KILLED " + GameCore.Instance.Score + " TANKS";
		}
		else
		{
			debrief.text = "YOU KILLED " + GameCore.Instance.Score + " TANK";
		}
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
