using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Animator anim;
    public float turnVelocity = 4f;
    public float shotsDelay = 0.2f;
    private float timeSinceLastShot = 0;
    public GameObject shot;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameControl.instance.gameOver)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameControl.instance.currVelocity += GameControl.instance.gainVelocity;
                if (GameControl.instance.currVelocity > GameControl.instance.maxVelocity)
                {
                    GameControl.instance.currVelocity = GameControl.instance.maxVelocity;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), turnVelocity);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), -turnVelocity);
            }
            if (GameControl.instance.currVelocity >= GameControl.instance.maxVelocity)
            {
                GameControl.instance.currVelocity = GameControl.instance.maxVelocity;
            }
            GameControl.instance.currVelocity -= GameControl.instance.gainVelocity / 3;
            if (GameControl.instance.currVelocity < 0)
            {
                GameControl.instance.currVelocity = 0;
            }
            anim.Play("PlayerMovement", 0, GameControl.instance.currVelocity / GameControl.instance.maxVelocity);
            if (timeSinceLastShot != 0)
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= shotsDelay)
                {
                    timeSinceLastShot = 0;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (timeSinceLastShot == 0)
                {
                    timeSinceLastShot += Time.deltaTime;
                    Instantiate(shot, new Vector3(Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.z) / 9 * 4, Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.z) / 9 * 4, 0), transform.rotation);
                }
            }
        }
        else
        {
            anim.Play("PlayerMovement", 0, 0);
            GameControl.instance.currVelocity = 0;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameControl.instance.GotHit();
    }
}
