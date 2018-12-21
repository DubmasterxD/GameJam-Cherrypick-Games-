using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{

    private int state;
    private Animator anim;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        SetAnimationState();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            gameObject.transform.LookAt(player);
        }
    }

    void SetAnimationState()
    {
        anim = gameObject.GetComponent<Animator>();
        state = Random.Range(0, 2);
        anim.SetInteger("State", state);
        if (state == 1)
        {
            gameObject.transform.position = gameObject.transform.position - new Vector3(0, 0.9f, 0);
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        if (state == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
