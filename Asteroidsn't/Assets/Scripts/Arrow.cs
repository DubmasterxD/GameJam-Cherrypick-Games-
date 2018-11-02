using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Animator anim;
    public float turnVelocity = 2f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GameControl.instance.currVelocity += GameControl.instance.gainVelocity;
            if(GameControl.instance.currVelocity>GameControl.instance.maxVelocity)
            {
                GameControl.instance.currVelocity = GameControl.instance.maxVelocity;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), turnVelocity);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), -turnVelocity);
        }
        if(GameControl.instance.currVelocity>= GameControl.instance.maxVelocity)
        {
            GameControl.instance.currVelocity = GameControl.instance.maxVelocity;
        }
        GameControl.instance.currVelocity -= GameControl.instance.gainVelocity/3;
        if(GameControl.instance.currVelocity<0)
        {
            GameControl.instance.currVelocity = 0;
        }
        anim.Play("PlayerMovement", 0, GameControl.instance.currVelocity / GameControl.instance.maxVelocity);
	}
}
