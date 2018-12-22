using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public float jumpForce;
    private Rigidbody rb;
    public int hp = 100;
    public Image blood;
    public Image blood2;
    public Collider standCollider;
    public Collider duckCollider;
    public GameObject gun;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical") * movementSpeed;
        Vector3 sideMovement = transform.right * Input.GetAxis("Horizontal") * movementSpeed / 2;
        Duck(forwardMovement, sideMovement);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                forwardMovement *= 2;
                sideMovement *= 2;
            }
        }
        rb.velocity = forwardMovement + sideMovement + new Vector3(0, 1, 0) * -9.8f;
    }

    private void Duck(Vector3 forwardMovement, Vector3 sideMovement)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            standCollider.enabled = false;
            duckCollider.enabled = true;
            gun.transform.localPosition = new Vector3(0, 2.091837f, 0.9152897f);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            forwardMovement /= 3;
            sideMovement /= 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            standCollider.enabled = true;
            duckCollider.enabled = false;
            gun.transform.localPosition = new Vector3(0, 9.091837f, 0.9152897f);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        UpdateScreen();
        if(hp<=0)
        {
            GameController.instance.PlayerDie();
        }
    }

    void UpdateScreen()
    {
        if(hp<50)
        {
            blood.gameObject.SetActive(true);
        }
        if(hp<20)
        {
            blood.gameObject.SetActive(false);
            blood2.gameObject.SetActive(true);
        }
    }
}
