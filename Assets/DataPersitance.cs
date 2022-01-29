using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataPersitance : MonoBehaviour
{
    public static DataPersitance Instance;
    public static string filePath = "/tutorial1.dat";


    public string curr_plyrName;
    public Dictionary<string, int> playerHighScores;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerHighScores = loadFromFile();
            if (playerHighScores  == null)
            {
                playerHighScores = new Dictionary<string, int>();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
    public void PlayerEnterWith(string plyrName)
    {
        if (playerHighScores.ContainsKey(plyrName) == false)
        {
            playerHighScores.Add(plyrName, 0);
        }
        curr_plyrName = plyrName;
    }
    public void saveScoreIfIsHighScore(int score)
    {
        if (playerHighScores.ContainsKey(curr_plyrName))
        {
            if (playerHighScores[curr_plyrName] < score)
            {
                playerHighScores[curr_plyrName] = score;
            }
        }
        else
        {            
            playerHighScores.Add(curr_plyrName, score);
        }
    }

    public void saveScore(string curr_plyrName, int score)
    {
        if (playerHighScores.ContainsKey(curr_plyrName))
        {
            playerHighScores[curr_plyrName] = score;
        }
        else
        {
            playerHighScores.Add(curr_plyrName, score);
        }
    }

    public int GetHighScore(string plyrName)
    {
        if (playerHighScores.ContainsKey(plyrName))
        {            
            return playerHighScores[plyrName];            
        }
        playerHighScores.Add(plyrName, 0);
        return 0;
    }


    public Dictionary<string, int> loadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + filePath))
        {
            FileStream file = File.Open(Application.persistentDataPath + filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            SaveInfo fileObject = (SaveInfo)bf.Deserialize(file);
            file.Close();

            return fileObject.playerScore;
        }
        return null;
    }
    public void saveToFile()
    {
        SaveInfo fileObject = new SaveInfo();
        fileObject.playerScore = Instance.playerHighScores;

        FileStream file = File.Open(Application.persistentDataPath + filePath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, fileObject);
        file.Close();
    }
    private void OnApplicationQuit()
    {
        saveToFile();
    }
}


[System.Serializable]
public class SaveInfo
{
    public Dictionary<string, int> playerScore;
}