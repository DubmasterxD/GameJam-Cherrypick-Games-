using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour
{
    public bool isMoving = false;               //If the column will move
    public float speed = 0.02f;                 //Speed and direction of the column
    private void Update()
    {
        //If the column is moving...
        if (isMoving)
        {
            //...and is going too far up or down...
            if (this.transform.position.y > 3.5f || this.transform.position.y < -0.5)
            {
                //...make it go in other direction...
                speed *= -1;
            }
            //...and adjust its position
            this.transform.Translate(new Vector2(0, speed));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bird>() != null)
        {
            //If the bird hits the trigger collider in between the columns then
            //tell the game control that the bird scored.
            GameControl.instance.BirdScored();
        }
    }
}
