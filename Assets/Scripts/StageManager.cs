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
        StartStage(currentStage);
    }

    public void StartStage(int stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage);

        // 카드 데이터 생성: 0,0,1,1,2,2,3,3... (짝 맞추기용)
        int[] cardData = new int[cardCount];
        for (int i = 0; i < cardCount; i++) cardData[i] = i / 2;
        cardData = cardData.OrderBy(x => Random.Range(0f, 1f)).ToArray(); // 셔플

        board.SetupBoard(stage, cardData); // ? 카드 배치 책임을 Board로 넘김

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
    
}
