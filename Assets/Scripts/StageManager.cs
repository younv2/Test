using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject cardPrefab; //카드 프리팹 담는곳
    public Transform cardParent; //어떤 오브젝트에 넣을지

    public enum StageType { Stage1, Stage2, Stage3, Stage4 }
    public StageType currentStage = StageType.Stage1; //현재 스테이지가 어디인지 저장해두는 변수

    void Start()
    {
        StartStage(currentStage);
    }

    public void StartStage(StageType stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage); //게임 시작되자마자 몇장의 카드가 필요한지 확인.
        GenerateCards(cardCount); //그 수만큼 카드 생성

        GameManager.stage = (int)stage + 1; // GameManager에 현재 스테이지 숫자 전달 (Stage1 == 0이므로 +1)
        GameManager.instance.cardCount = cardCount; // GameManager에 카드 수 전달
    }

    int GetCardCount(StageType stage)
    {
        switch (stage)
        {
            case StageType.Stage1:
            case StageType.Stage2:
                return 8; //스테이지1,2에서는 8장
            case StageType.Stage3:
                return 12;//스테이지3은 12장
            case StageType.Stage4:
                return 16;//스테이지4는 16장
            default:
                return 8;//잘못된 값 불러올 경우 기본값 8장 반환.
        }
    }

    void GenerateCards(int count) //함수가 호출될 때 얼마나 많은 카드를 만들지 정수값으로 전달
    {
        for (int i = 0; i < count; i++) //count 만큼 반복해서 아래 코드를 실행
        {
            Instantiate(cardPrefab, cardParent); //프리팹을 씬 안에 새로 생성하는 함수
            //cardParent아래로 8개 생성.
        }
    }

    void ClearCards() //cardParent 안에 있는 모든 오브젝트를 하나씩 삭제
    {
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
    }

    // 임시) 버튼에서 호출할 수 있게 만든 메서드
    public void OnClickStage1() => StartStage(StageType.Stage1);
    public void OnClickStage2() => StartStage(StageType.Stage2);
    public void OnClickStage3() => StartStage(StageType.Stage3);
    public void OnClickStage4() => StartStage(StageType.Stage4);
}
