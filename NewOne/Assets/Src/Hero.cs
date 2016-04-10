using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 10.0f;

	[SerializeField]
	Transform itemPosition;

	Rigidbody2D body;

	Module currentModule;
	bool facingRight = true;

	Item holdedItem;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();	
	}

	void Update()
	{
		if (currentModule != null && Input.GetKeyDown(KeyCode.Space))
		{
			
			var dispenser = currentModule as DispenserModule;
			if (dispenser!=null)
			{
				if(holdedItem!=null)
				{
					TopPanel.Instance.setStatus("Руки уже заняты");
					return;
				}

				if (!PickUpItem(dispenser.GetItem()))
	            {
					TopPanel.Instance.setStatus("Нечего брать");
				}
				
				return;         
			}

			var shaft = currentModule as ShaftModule;
			if (shaft != null)
			{
				if (shaft.StoreItem(holdedItem))
					holdedItem = null;
				return;
			}

			currentModule.SetTrigger();
		}
	}

//INVENTORY
	bool PickUpItem(Item item)
	{
		if(holdedItem!=null || item == null)
			return false;
		holdedItem = item;
		item.transform.parent = gameObject.transform;
		item.transform.position = itemPosition.transform.position;
        return true;
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");

		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);

		if (move > 0 && !facingRight)
		{
			Flip();
		}
		else if (move < 0 & facingRight)
		{
			Flip();
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		currentModule = otherCollider.gameObject.GetComponent<Module>();
		
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		currentModule = null;
    }

	void Flip()
	{
		facingRight = !facingRight;
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
