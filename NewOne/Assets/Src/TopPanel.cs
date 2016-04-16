using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour {

	[SerializeField]
	Text status;

	[SerializeField]
	Text hp;

	[SerializeField]
	Text score;

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
		if (hp!=null && GameCore.Instance.mainTank!=null)
		{
			hp.text = "HP: " + GameCore.Instance.mainTank.HP;
		}

		if (score != null)
		{
			score.text = "Deathcount: " + GameCore.Instance.Score;
		}


	
	}
}
