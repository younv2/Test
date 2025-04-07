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
                endTxt.SetActive(true);
                if(GetStarScore() == 3)
                {
                    StageClear();
                }
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();

            life--;
            if(life <= 0)
            {
                GameOver();
            }
        }
        firstCard = null;
        secondCard = null;
    }

    public void StageClear()
    {
        if (PlayerPrefs.GetInt("clearMaxStage") < stage + 1)
        {
            PlayerPrefs.SetInt("clearMaxStage", stage + 1);
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }

    private int GetStarScore()
    {
        int starScore = 0;
        if(time <= 15f && life >= 3)
        {
            starScore = 3;
        }
        else if (time <= 20f && life >= 2)
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
}
