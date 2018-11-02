﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public float maxVelocity=0.3f;
    public float currVelocity=0;
    public float gainVelocity=0.02f;
    public Vector2 move = new Vector2(0,0);

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        move[0] -= Mathf.Cos(Mathf.Deg2Rad * GameObject.FindGameObjectWithTag("Player").gameObject.transform.localEulerAngles.z) * currVelocity * Time.deltaTime;
        move[1] -= Mathf.Sin(Mathf.Deg2Rad * GameObject.FindGameObjectWithTag("Player").gameObject.transform.localEulerAngles.z) * currVelocity * Time.deltaTime;
        if (Mathf.Pow(move[0], 2) + Mathf.Pow(move[1], 2) > maxVelocity)
        {
            move[0] = -Mathf.Cos(Mathf.Deg2Rad * GameObject.FindGameObjectWithTag("Player").gameObject.transform.localEulerAngles.z) * currVelocity ;
            move[1] = -Mathf.Sin(Mathf.Deg2Rad * GameObject.FindGameObjectWithTag("Player").gameObject.transform.localEulerAngles.z) * currVelocity ;
        }
        move[0] -= move[0] / 6 * 5 * Time.deltaTime;
        move[1] -= move[1] / 6 * 5 * Time.deltaTime;
    }
}
