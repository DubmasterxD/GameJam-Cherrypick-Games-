using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public static GameController instance;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDie()
    {
        score -= 5000;
        SceneManager.LoadScene("SubmitScore");
    }

    public void Win(float timer)
    {
        score += (int)(60 / timer * 1000);
        SceneManager.LoadScene("SubmitScore");
    }
}
