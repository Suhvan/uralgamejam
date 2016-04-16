using UnityEngine;
using System.Collections;

public class ShaftModule : Module {

	[SerializeField]
	Transform itemPosition;

	Item storedItem;

	public bool StoreItem(Item it)
	{
		if(storedItem!=null || it == null)
			return false;

		storedItem = it;

		storedItem.transform.parent = gameObject.transform;
		storedItem.transform.position = itemPosition.transform.position;

		if (storedItem.transform.localScale.x < 0)
		{	
			var scale = storedItem.transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
		
		return true;
	}


	public Projectile GetBullet()
	{
		if (storedItem == null)
			return null;
		var item = storedItem.Prefab;
		Destroy(storedItem.gameObject);
		storedItem = null;
        return item;
    }
		 

	// Use this for initialization
	protected override void Start () {
	
	}

	// Update is called once per frame
	protected override void Update () {
	
	}
}
