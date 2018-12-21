using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float timer = 0;
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        gameObject.GetComponent<Text>().text = (int)(timer / 60) + ":" + Mathf.FloorToInt(timer % 60);
	}
}
