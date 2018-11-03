using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour {

    public float lifetime = 3;
    private float timeAlive = 0;
    public float speed = 2;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.z) * speed, Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.z) * speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive>=lifetime)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameControl.instance.AddPoint();
        Destroy(this.gameObject);
    }
}
