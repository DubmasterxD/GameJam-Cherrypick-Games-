using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSubmit : MonoBehaviour {

    public Text nick;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Submit()
    {
        if(nick.text.Length>15)
        {
            nick.text = "Nickname too long";
        }
        else
        {
            ScoreboardManager.instance.AddScore(nick.text, GameController.instance.score);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
