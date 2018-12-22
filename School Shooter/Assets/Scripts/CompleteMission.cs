using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMission : MonoBehaviour {

    private Score score;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameController.instance.Win(score.timer);
        }
    }
}
