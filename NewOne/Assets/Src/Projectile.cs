using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[SerializeField]
	public float shotForce = 10;

    [SerializeField]
    float tracingInterval = 0.2f;
    float tracingTimer = 0.0f;

    [SerializeField]
    Tracer tracerPrefab;

	[SerializeField]
	int Damage = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // rotate it to match velocity
        Vector2 vNorm = GetComponent<Rigidbody2D>().velocity.normalized;
        this.transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), vNorm);
        //Debug.DrawRay(this.transform.position, vNorm, Color.red);

        // leave a trace once in a while
        tracingTimer += Time.deltaTime;
        if( tracingTimer > tracingInterval ) {
            tracingTimer -= tracingInterval;
            if( tracerPrefab )
                Instantiate(tracerPrefab, this.transform.position, this.transform.rotation); 
        }
	}

	public void Kickstart(float gunForce)
	{	
        float power = shotForce + gunForce;

        GetComponent<Rigidbody2D>().AddForce(this.transform.rotation * new Vector2(power, 0), ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		var mortalOne = other.gameObject.GetComponent<Mortal>();
		if (mortalOne != null)
		{
			mortalOne.GetDamage(Damage);
        }
		Destroy(gameObject);
	}
}
