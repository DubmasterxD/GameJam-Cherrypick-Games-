using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public int score = 0;
    private float timer = 0;
    private float timeSinceLastStudentKill = 0;
    private int killCombo = 0;
    private float multiTextTimer=0;
    public Text multikillText;

    private void Awake()
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

    private void Update()
    {
        timer += Time.deltaTime;
        timeSinceLastStudentKill += Time.deltaTime;
        multiTextTimer += Time.deltaTime;
        if (multiTextTimer > 3 && multikillText.gameObject.active)
        {
            multikillText.gameObject.SetActive(false);
        }
    }

    public void StudentKill()
    {
        killCombo++;
        if (timeSinceLastStudentKill < 6)
        {
            score += killCombo * 1000;
        }
        else if (timeSinceLastStudentKill < 12)
        {
            score += (int)(1000 + 1000*(killCombo - 1) * (10 - timeSinceLastStudentKill));
        }
        else
        {
            killCombo = 1;
            score += 1000;
        }
        if (killCombo == 5)
        {
            score += 10000;
        }
        MultikillUpdate();
        gameObject.GetComponent<Text>().text = score.ToString();
        timeSinceLastStudentKill = 0;
    }

    void MultikillUpdate()
    {
        switch (killCombo)
        {
            case 1:
                multikillText.text = "";
                break;
            case 2:
                multikillText.text = "Doublekill";
                break;
            case 3:
                multikillText.text = "Triplekill";
                break;
            case 4:
                multikillText.text = "Quadrakill!";
                break;
            case 5:
                multikillText.text = "Pentakill!!";
                break;
            case 6:
                multikillText.text = "Godlike!!";
                break;
            case 7:
                multikillText.text = "LEGENDARY!!!";
                break;
            default:
                break;
        }
        multikillText.gameObject.SetActive(true);
        multiTextTimer = 0;
    }

    public void EndGame()
    {
        score += (int)(180 / timer * 1000);
    }
}
