using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {

	[SerializeField]
	public Hero Player;

	[SerializeField]
	public Tank mainTank;

	public int Score;

	public static GameCore Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GameOver()
	{
		TopPanel.Instance.setStatus("GAME OVER");
		Time.timeScale = 0;
	}


	
}
