using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;			//Has the player collided with a wall?

	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.

	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
			//Look for input to trigger a "flap".
			if (Input.GetMouseButtonDown(0)) 
			{
				//...tell the animator about it and then...
				anim.SetTrigger("Flap");
				//...zero out the birds current y velocity before...
				rb2d.velocity = Vector2.zero;
				//	new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.
				rb2d.AddForce(new Vector2(0, upForce));
			}
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        // Zero out the bird's velocity
        rb2d.velocity = Vector2.zero;
        //Decreases the number of lifes left...
        GameControl.instance.lifesLeft--;
        if(GameControl.instance.lifesLeft<0)
        {
            GameControl.instance.lifesLeft = 0;
        }
        //...and tells the game about it.
        GameControl.instance.BirdGotHit();
        // If the player has any lifes left...
        if (GameControl.instance.lifesLeft > 0)
        {
            //...checks if the bird hit column...
            if (other.gameObject.name != "Ground")
            {
                //...if it did, then stop the column if it's moveing and move column away...
                other.gameObject.GetComponentInParent<Column>().isMoving = false;
                other.gameObject.GetComponentInParent<Rigidbody2D>().MovePosition(new Vector2(60, other.gameObject.GetComponent<Rigidbody2D>().position.y));
            }
            //...else if it hit ground...
            else
            {
                //...move bird back to starting position.
                rb2d.MovePosition(new Vector2(-1.8f, 0.5583044f));
            }
        }
        // If the bird collides with something and is out of lifes...
        else
        {
            //...set it to dead...
            isDead = true;
            //...tell the Animator about it...
            anim.SetTrigger("Die");
            //...and tell the game control about it.
            GameControl.instance.BirdDied();
        }
    }
}
