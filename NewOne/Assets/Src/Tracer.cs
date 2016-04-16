using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour {

    [SerializeField]
    float duration = 5.0f;

    float lifetime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime += Time.deltaTime;
        // update alpha on a sprite
        float a = lifetime / duration;
        Color newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 1 - Mathf.Clamp(a, 0, 1);
        GetComponent<SpriteRenderer>().color = newColor;

        if (lifetime > duration)
            Destroy(gameObject);
	}
}
