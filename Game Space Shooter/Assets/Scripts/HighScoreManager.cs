using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager scoreManager;

    //public static HighScore[] highScores;
    public static int[] highScoresStatic = { 0, 0, 0, 0, 0};
    int[] highScores = { 0, 0, 0, 0, 0};

    private void Awake()
    {
        if (scoreManager && scoreManager != this)
            Debug.LogError("Co nhien hon 1 Score Manager");
        else
            scoreManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();

        for (int i = 0; i < 5; i++)
        {
            highScores[i] = highScoresStatic[i];
        }

    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore0", highScores[0]);
        PlayerPrefs.SetInt("HighScore1", highScores[1]);
        PlayerPrefs.SetInt("HighScore2", highScores[2]);
        PlayerPrefs.SetInt("HighScore3", highScores[3]);
        PlayerPrefs.SetInt("HighScore4", highScores[4]);
    }

    public void GetHighScore()
    {
        highScoresStatic[0] = PlayerPrefs.GetInt("HighScore0");
        highScoresStatic[1] = PlayerPrefs.GetInt("HighScore1");
        highScoresStatic[2] = PlayerPrefs.GetInt("HighScore2");
        highScoresStatic[3] = PlayerPrefs.GetInt("HighScore3");
        highScoresStatic[4] = PlayerPrefs.GetInt("HighScore4");
    }

    public void CompareHighScore(int newScore)
    {
        for (int i = 0; i < 5; i++)
        {
            if (newScore > highScores[i])
            {
                for (int j = 4; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                highScores[i] = newScore;
                break;
            }
        }
    }
}
