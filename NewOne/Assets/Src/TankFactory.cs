using UnityEngine;
using System.Collections;

public class TankFactory : MonoBehaviour {

	[SerializeField]
	GameObject tankPrefab;

	[SerializeField]
	float spawnCooldown = 20;
	float spawnTimer = 0;

	[SerializeField]
	Transform leftBase;

	[SerializeField]
	Transform rightBase;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if (spawnTimer > spawnCooldown)
		{
			spawnTimer = 0;
			SpawnTank();
        }
	}

	void SpawnTank()
	{
		var pos = rightBase.transform.position;
		pos.z = 0;
		Instantiate(tankPrefab, pos, Quaternion.identity);
	}
}
