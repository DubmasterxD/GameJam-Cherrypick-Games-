using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text nicknames;
    public Text scores;

	public void StartGame()
    {
        GameController.instance.score = 0;
        SceneManager.LoadScene("Map");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void WriteScores()
    {
        nicknames.text = "Nickname:" +
                                "\n" + ScoreboardManager.instance.scoreboard[0].name +
                                "\n" + ScoreboardManager.instance.scoreboard[1].name +
                                "\n" + ScoreboardManager.instance.scoreboard[2].name +
                                "\n" + ScoreboardManager.instance.scoreboard[3].name +
                                "\n" + ScoreboardManager.instance.scoreboard[4].name +
                                "\n" + ScoreboardManager.instance.scoreboard[5].name +
                                "\n" + ScoreboardManager.instance.scoreboard[6].name;
        scores.text = "Score:" +
                          "\n" + ScoreboardManager.instance.scoreboard[0].score +
                          "\n" + ScoreboardManager.instance.scoreboard[1].score +
                          "\n" + ScoreboardManager.instance.scoreboard[2].score +
                          "\n" + ScoreboardManager.instance.scoreboard[3].score +
                          "\n" + ScoreboardManager.instance.scoreboard[4].score +
                          "\n" + ScoreboardManager.instance.scoreboard[5].score +
                          "\n" + ScoreboardManager.instance.scoreboard[6].score;
    }
}
