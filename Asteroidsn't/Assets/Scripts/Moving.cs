using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {
    	
	// Update is called once per frame
	void Update ()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position[0] + GameControl.instance.move[0] * 10 * Time.deltaTime, transform.position[1] + GameControl.instance.move[1] * 10 * Time.deltaTime, transform.position[2]), transform.rotation);
	}
}
