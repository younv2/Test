using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text timeTxt;
    public GameObject endTxt;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;
    private float time = 0f;

    public Text stageTxt;

    public static int stage = 0;

    public int life = 10;
    public Text lifeText;

    public GameObject dimPanel;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Time.timeScale = 1f;
        lifeText.text = life.ToString();
        Debug.Log("[GameManager] life: " + life);
        Debug.Log("[GameManager] stage: " + stage);
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time > 30.0f)
        {
            GameOver();
        }
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                Time.timeScale = 0f;
                StageClear();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            DownLife(); 
            if(life <= 0)
            {
                GameOver();
            }
        }
        firstCard = null;
        secondCard = null;
    }

    private void NextStageOpen()
    {
        if (PlayerPrefs.GetInt("clearMaxStage") < stage + 1)
        {
            PlayerPrefs.SetInt("clearMaxStage", stage + 1);
        }
    }

    private void StageClear()
    {
        if(GetStarScore() == 3)
        {
            NextStageOpen();
        }
        GameEnd(true);
    }

    private void GameOver()
    {
        GameEnd(false);
    }

    private void GameEnd(bool isClear)
    {
        Time.timeScale = 0f;
        if (gameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is not assigned in the inspector!");
            return;
        }

        GameOverPanel panel = gameOverPanel.GetComponent<GameOverPanel>();
        if (panel == null)
        {
            Debug.LogError("GameOverPanel component is missing!");
            return;
        }

        dimPanel?.SetActive(true);  // null 체크와 함께 활성화
        gameOverPanel.SetActive(true);
        panel.ShowResult(isClear, GetStarScore());
    }

    private int GetStarScore()
    {
        int starScore = 0;
        if(time <= 27f && life >= 3)
        {
            starScore = 3;
        }
        else if (time <= 28f && life >= 2)
        {
            starScore = 2;
        }
        else
        {
            starScore = 1;
        }
        Debug.Log("[GameManager] starScore: " + starScore);
        return starScore;
    }   
    
    private void DownLife() {
        life--;
        lifeText.text = life.ToString();
    }
}
