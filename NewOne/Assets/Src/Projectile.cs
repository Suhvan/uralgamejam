using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[SerializeField]
	float shotForce = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Kickstart(float gunForce)
	{	
        float power = shotForce + gunForce;

        GetComponent<Rigidbody2D>().AddForce(this.transform.rotation * new Vector2(power, 0), ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Destroy(gameObject);
	}
}
