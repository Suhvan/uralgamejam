using UnityEngine;
using System.Collections;

public class DispenserModule : Module {

	[SerializeField]
	private Item itemPrefab;

	[SerializeField]
	protected Transform spawnPoint;

	[SerializeField]
	float spawnCooldown = 3;

	Item spawnedObject;

	float spawnTimer;
	

	// Use this for initialization
	protected override void Start () {
	
	}

	public Item GetItem()
	{
		var item = spawnedObject;
		spawnedObject = null;
		return item;
	}

	// Update is called once per frame
	protected override void Update () {

		if (spawnedObject == null)
		{
			spawnTimer += Time.deltaTime;
			if (spawnTimer > spawnCooldown)
			{
				spawnTimer = 0;
				spawnedObject = (Item)Instantiate( itemPrefab, spawnPoint.position, Quaternion.identity);
				spawnedObject.transform.parent = gameObject.transform;
            }
		}
	
	}
}
