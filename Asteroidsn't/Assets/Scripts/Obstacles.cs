using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public float speedMin = 0.5f;
    public float speedMax = 4f;
    private float speed;
    private Vector2 direction = new Vector2();
    private Rigidbody2D rb;
    public float edge = 5.07f;
    private float inactiveObstaclesXLimit = 29;
    public enum Types { Triangle, Square};
    public Types type;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > edge && transform.position.x < inactiveObstaclesXLimit)
        {
            transform.SetPositionAndRotation(new Vector3(-edge, transform.position.y, 0), transform.rotation);
        }
        else if (transform.position.x > inactiveObstaclesXLimit && transform.position.x < inactiveObstaclesXLimit + 1)
        {
            transform.SetPositionAndRotation(new Vector3(inactiveObstaclesXLimit + 10, transform.position.y, 0), transform.rotation);
        }
        else if (transform.position.x>inactiveObstaclesXLimit+30)
        {
            transform.SetPositionAndRotation(new Vector3(inactiveObstaclesXLimit + 20, transform.position.y, 0), transform.rotation);
        }
        if (transform.position.x < -edge)
        {
            transform.SetPositionAndRotation(new Vector3(edge, transform.position.y, 0), transform.rotation);
        }
        if (transform.position.y > edge)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, -edge, 0), transform.rotation);
        }
        if (transform.position.y < -edge)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, edge, 0), transform.rotation);
        }
    }

    public void Randomize()
    {
        speed = Random.Range(speedMin, speedMax);
        direction[0] = Random.Range(-1f, 1f);
        direction[1] = Mathf.Sqrt(1 - Mathf.Pow(direction[0], 2));
        if (Random.Range(0, 2) == 0)
        {
            direction[1] *= -1;
        }
        rb.velocity = new Vector2(direction[0] * speed, direction[1] * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 tmp = Vector2.negativeInfinity;
            if(type==Types.Square)
            {
                tmp = transform.position;
            }
            transform.SetPositionAndRotation(new Vector3(inactiveObstaclesXLimit + 5, transform.position.y, 0), transform.rotation);
            if(tmp[0]!=float.NegativeInfinity)
            {
                GameControl.instance.gameObject.GetComponent<SpawningObstacles>().MakeTriangles(tmp);
            }
            GameControl.instance.gameObject.GetComponent<SpawningObstacles>().AddToList(this.gameObject);
        }
    }
}
