using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {

	[SerializeField]
	Knight playerPrefab;

	[SerializeField]
	public int funScore;

	[SerializeField]
	Transform spawnPoint;

	[SerializeField]
	GameObject Debrief;

	public static GameCore Instance
	{
		private set;
		get;
	}

	void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
			
	}

	public void Victory()
	{
		funScore += 200;

		Debrief.SetActive(true);

	}

	public void RespawnPlayer()
	{
		var newKnight = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
		funScore -= 100 ;
	
	}
}
