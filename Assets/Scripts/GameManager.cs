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
    private float limitTime = 30.0f;

    public Text stageTxt;
    public static int stage = 0;
    public int life = 10;
    public Text lifeText;

    public GameObject dimPanel;
    public GameObject gameOverPanel;

    // 카드 컬렉션 관련 변수
    public const string COLLECTION_KEY = "collections";
    private HashSet<int> collectedCards;

    // 시간 경고 관련 변수
    [Header("시간 경고 설정")]
    public float warningTime = 10f;        // 경고 남은 시간 (초)
    public string warningSound = "TimeWarning";  // 경고음 클립 이름
    private bool isWarningActive = false;
    private bool hasPlayedWarningSound = false;
    
    [Header("경고 색상 설정")]
    public Color warningColor = Color.red;
    public float blinkInterval = 0.2f;  // 깜빡임 간격 (초)
    private Color originalTextColor;
    private float nextBlinkTime = 0f;
    private bool isWarningColorOn = false;

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
        originalTextColor = timeTxt.color;
        hasPlayedWarningSound = false;
        Debug.Log("[GameManager] life: " + life);
        Debug.Log("[GameManager] stage: " + stage);
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        
        // 시간 경고 체크
        CheckTimeWarning();
        // 경고 상태일 때 깜빡임 효과
        if (isWarningActive && Time.time >= nextBlinkTime)
        {
            isWarningColorOn = !isWarningColorOn;
            timeTxt.color = isWarningColorOn ? warningColor : originalTextColor;
            nextBlinkTime = Time.time + blinkInterval;
        }

        if (time > limitTime)
        {
            GameOver();
        }
    }

    private void CheckTimeWarning()
    {
        float remainingTime = limitTime - time;
        if (remainingTime <= warningTime && !isWarningActive)
        {
            Debug.Log("[GameManager] 경고 시작");
            // 경고 시작
            isWarningActive = true;
            isWarningColorOn = true;
            timeTxt.color = warningColor;
            nextBlinkTime = Time.time + blinkInterval;
            
            // 경고음을 아직 재생하지 않았을 때만 재생
            if (!hasPlayedWarningSound && SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(SoundType.SFX, warningSound, false);
                hasPlayedWarningSound = true;
            }
        }
        else if (remainingTime > warningTime && isWarningActive)
        {
            // 경고 해제
            isWarningActive = false;
            timeTxt.color = originalTextColor;
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
