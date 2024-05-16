using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    [SerializeField] Text score;
    [SerializeField] Text scoreGO;
    [SerializeField] Text rewardText;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject nextLevelPanel;

    void Awake()
    {
        if (uIManager && uIManager != this)
        {
            Debug.LogError("Loi nhieu UI Manager");
        }
        else
            uIManager = this;
    }

    public void SetScoreText(string s)
    {
        if (score)
        {
            score.text = s;
        }
    }

    public void SetScoreGOText(string s)
    {
        if (scoreGO)
        {
            scoreGO.text = s;
        }
    }

    public void SetGameoverState(bool state)
    {
        gameoverPanel.SetActive(state);
    }

    public void SetPauseState(bool state)
    {
        pausePanel.SetActive(state);
    }

    public void SetNextLevelState(bool state)
    {
        nextLevelPanel.SetActive(state);
    }

    public void SetRewardText(string s)
    {
        rewardText.text = s;
    }    
}
