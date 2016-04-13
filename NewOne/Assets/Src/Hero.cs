﻿using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 10.0f;

	[SerializeField]
	float jumpForce = 20;

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

	void useModule()
	{
		var dispenser = currentModule as DispenserModule;
		if (dispenser != null)
		{
			if (holdedItem != null)
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

	void Update()
	{
		if (currentModule != null && Input.GetKeyDown(KeyCode.Space))
		{
			useModule();
        }

		if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
		{
			body.AddForce(new Vector2(0, jumpForce));
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
