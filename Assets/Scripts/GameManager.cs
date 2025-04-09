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

    // 카드 컬렉션 관련 변수
    public const string COLLECTION_KEY = "collections";
    private HashSet<int> collectedCards;

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
        stageTxt.text = stage.ToString();
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

    // add and save card to collection
    private void SaveCardToCollection()
    {
        string collectionData = PlayerPrefs.GetString(COLLECTION_KEY, "");
        HashSet<int> collectionCards = new HashSet<int>();
        
        if (!string.IsNullOrEmpty(collectionData))
        {
            string[] cardIds = collectionData.Split(',');
            foreach (string id in cardIds)
            {
                if(int.TryParse(id, out int parsedId))
                {
                    collectionCards.Add(parsedId);
                }
            }
        }

        if(collectionCards.Count == 4)
        {
            Debug.Log("[GameManager] 모든 카드를 모았습니다.");
            return;
        }

        int idx = 1;
        while(idx <= 4)
        {
            if(!IsCollected(idx))
            {
                break;
            }
            idx++;
        }

        if (collectionCards.Add(idx))
        {
            string newCollectionData = string.Join(",", collectionCards);
            PlayerPrefs.SetString(COLLECTION_KEY, newCollectionData);
            PlayerPrefs.Save();
            Debug.Log($"[GameManager] New card {idx} added to collection!");
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
        if (stage == 9)
        {
            SaveCardToCollection();
        }

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

        dimPanel?.SetActive(true);
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
    
    private void DownLife()
    {
        life--;
        lifeText.text = life.ToString();
    }

    private bool IsCollected(int idx)
    {
        string collectionData = PlayerPrefs.GetString(COLLECTION_KEY, "");
        return collectionData.Contains(idx.ToString());
    }

    private void ClearCollection()
    {
        PlayerPrefs.DeleteKey(COLLECTION_KEY);
    }
}
