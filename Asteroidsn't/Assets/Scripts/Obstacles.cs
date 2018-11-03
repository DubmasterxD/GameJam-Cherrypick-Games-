using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public float speed;
    public Vector2 direction;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Randomize();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void Randomize()
    {
        speed = Random.Range(0.5f, 4f);
        direction[0] = Random.Range(-1f, 1f);
        direction[1] = Mathf.Sqrt(1 - Mathf.Pow(direction[0], 2));
        if (Random.Range(0, 2) == 0)
        {
            direction[1] *= -1;
        }
        rb.velocity = new Vector2(direction[0] * speed, direction[1] * speed);
    }
}
