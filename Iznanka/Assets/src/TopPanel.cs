using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour {
	[SerializeField]
	Text funPointsLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		funPointsLabel.text = "FUN POINTS: " + GameCore.Instance.funScore;
    }
}
