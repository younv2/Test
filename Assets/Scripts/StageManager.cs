using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject cardPrefab; //카드 프리팹 담는곳
    public Transform cardParent; //어떤 오브젝트에 넣을지

    public int currentStage = 1; // 스테이지 숫자 (1 ~ 8)
    public Board board;

    void Start()
    {
        currentStage = GameManager.stage;
        StartStage(currentStage);
    }

    public void StartStage(int stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage);
        int pairCount = cardCount / 2;

        // 전체 카드 ID 풀 (0~15 중에서 랜덤하게 pairCount개 뽑기)
        List<int> allCardIds = Enumerable.Range(0, 16).OrderBy(x => Random.Range(0f, 1f)).Take(pairCount).ToList();

        List<int> cardDataList = new List<int>();
        foreach (int id in allCardIds)
        {
            cardDataList.Add(id); // 1장
            cardDataList.Add(id); // 짝
        }

        // 셔플
        cardDataList = cardDataList.OrderBy(x => Random.Range(0f, 1f)).ToList();

        board.SetupBoard(stage, cardDataList.ToArray());

        GameManager.stage = stage;
        GameManager.instance.cardCount = cardCount;
    }




    int GetCardCount(int stage)
    {
        switch (stage)
        {
            case 1:
            case 2:
            case 3:
                return 8; //스테이지1~3에서는 8장
            case 4:
            case 5:
            case 6:
                return 12;//스테이지4~6은 12장
            case 7:
            case 8:
                return 16;//스테이지7~8은 16장
            case 9:
                return 8;//히든스테이지는 8장으로 구성
            default:
                return 8;//잘못된 값 불러올 경우 기본값 8장 반환.
        }
    }

   


    void ClearCards() //cardParent 안에 있는 모든 오브젝트를 하나씩 삭제
    {
        for (int i = cardParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(cardParent.GetChild(i).gameObject);
        }
    }

    // 임시) 버튼에서 호출할 수 있게 만든 메서드
    public void OnClickStage1() => StartStage(1); //int로 변경

    // 히든 스테이지 해금 처리
    public void UnlockHiddenStage()
    {
        PlayerPrefs.SetInt("HiddenStageUnlocked", 1);
        PlayerPrefs.Save();
    }

    public bool IsHiddenStageUnlocked()
    {
        int clearedStage = PlayerPrefs.GetInt("clearMaxStage", 1);
        return clearedStage >= 8;
    }
}
