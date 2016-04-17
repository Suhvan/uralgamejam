using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 10.0f;

	[SerializeField]
	float jumpForce = 20;

	[SerializeField]
	float jumpAddForce = 20;

	[SerializeField]
	Transform itemPosition;

	[SerializeField]
	Transform groundCheck;

	//TODO: очевидно надо перенести это в какой то конфиг
	[SerializeField]
	public LayerMask WhatIsGround;

	[SerializeField]
	public int playerLayer;

	[SerializeField]
	public int platformLayer;

	[SerializeField]
	float jumpCooldown = 0f;

	bool grounded = false;

	float groundRadius = 0.02f;
	Rigidbody2D body;
	Module currentModule;
	bool facingRight = true;

	//TODO: написать бы класс - инвентарь
	Item holdedItem;

	public float getYPos()
	{
		return groundCheck.position.y;
	}

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();	
	}

	void autoUseModule()
	{
		var dispenser = currentModule as DispenserModule;
		if (dispenser != null)
		{
			if (holdedItem != null)
			{
				//TopPanel.Instance.setStatus("Руки уже заняты");
				return;
			}

			if (!PickUpItem(dispenser.GetItem()))
			{
				//TopPanel.Instance.setStatus("Нечего брать");
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
	}

	void useModule()
	{
		currentModule.SetTrigger();
	}

	void Update()
	{
		if (currentModule != null && Input.GetKeyDown(KeyCode.Space))
		{
			useModule();
        }

		if (jumpCooldown > 0) {
			jumpCooldown -= Time.deltaTime;
			jumpCooldown = Mathf.Max (jumpCooldown, 0);
		}

		if (jumpCooldown <= 0 && grounded && Input.GetKey(KeyCode.UpArrow))
		{
			body.velocity = Vector2.zero;
			body.AddForce(new Vector2(0, jumpForce));
			jumpCooldown = 0.2f;
		}
	}

	//TODO: написать бы класс - инвентарь
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

		if (jumpCooldown > 0 && Input.GetKey(KeyCode.UpArrow))
		{
			body.AddForce(new Vector2(0, jumpAddForce));
		}

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, WhatIsGround);

		//Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, body.velocity.y > 0);

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

		var module = otherCollider.gameObject.GetComponent<Module>();
		if (module != null)
		{
			currentModule = module;
			autoUseModule();
            return;
		}

		var button = otherCollider.gameObject.GetComponent<Button>();
		if(button!=null)
		{
			button.Press();
			return;
		}

	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{

		var module = otherCollider.gameObject.GetComponent<Module>();
		if (module != null)
		{
			currentModule = null;
			return;
		}

		var button = otherCollider.gameObject.GetComponent<Button>();
		if (button != null)
		{
			button.Release();
			return;
		}

		
    }


	void Flip()
	{
		facingRight = !facingRight;
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
