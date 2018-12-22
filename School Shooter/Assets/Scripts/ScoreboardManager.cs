using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class PlayerScore : IComparable
{
    public string name;
    public int score;

    public PlayerScore()
    {
        name = "";
        score = 0;
    }

    public PlayerScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public int CompareTo(object obj)
    {
        PlayerScore p = (PlayerScore)obj;
        return (score - p.score);
    }
}

public class ScoreboardManager : MonoBehaviour {
    
    public List<PlayerScore> scoreboard;
    public static ScoreboardManager instance;

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            ResetScores();
            DontDestroyOnLoad(gameObject);
            LoadScores();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
    public void AddScore(string nick, int score)
    {
        scoreboard.Add(new PlayerScore(nick, score));
        scoreboard.Sort();
        scoreboard.Reverse();
        scoreboard.RemoveAt(scoreboard.Count - 1);
        SaveScores();
    }

    public void LoadScores()
    {
        string destination = Application.persistentDataPath + "/scores.dat";
        FileStream fs;
        if(File.Exists(destination))
        {
            fs = File.OpenRead(destination);
        }
        else
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        scoreboard = (List<PlayerScore>)bf.Deserialize(fs);
        fs.Close();
    }

    public void SaveScores()
    {
        string destination = Application.persistentDataPath + "/scores.dat";
        FileStream fs;
        if(File.Exists(destination))
        {
            fs = File.OpenWrite(destination);
        }
        else
        {
            fs = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, scoreboard);
        fs.Close();
    }

    public void ResetScores()
    {
        scoreboard = new List<PlayerScore>();
        for (int i = 0; i < 7; i++)
        {
            scoreboard.Add(new PlayerScore());
        }
    }
}
