using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject cardPrefab; //ī�� ������ ��°�
    public Transform cardParent; //� ������Ʈ�� ������

    public int currentStage = 1; // �������� ���� (1 ~ 8)
    public Board board;

    void Start()
    {
        StartStage(currentStage);
    }

    public void StartStage(int stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage);

        // ī�� ������ ����: 0,0,1,1,2,2,3,3... (¦ ���߱��)
        int[] cardData = new int[cardCount];
        for (int i = 0; i < cardCount; i++) cardData[i] = i / 2;
        cardData = cardData.OrderBy(x => Random.Range(0f, 1f)).ToArray(); // ����

        board.SetupBoard(stage, cardData); // ? ī�� ��ġ å���� Board�� �ѱ�

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
                return 8; //��������1~3������ 8��
            case 4:
            case 5:
            case 6:
                return 12;//��������4~6�� 12��
            case 7:
            case 8:
                return 16;//��������7~8�� 16��
            default:
                return 8;//�߸��� �� �ҷ��� ��� �⺻�� 8�� ��ȯ.
        }
    }

   


    void ClearCards() //cardParent �ȿ� �ִ� ��� ������Ʈ�� �ϳ��� ����
    {
        for (int i = cardParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(cardParent.GetChild(i).gameObject);
        }
    }

    // �ӽ�) ��ư���� ȣ���� �� �ְ� ���� �޼���
    public void OnClickStage1() => StartStage(1); //int�� ����
    
}
