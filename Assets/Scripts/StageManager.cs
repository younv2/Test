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
        currentStage = GameManager.stage;
        StartStage(currentStage);
    }

    public void StartStage(int stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage);
        int pairCount = cardCount / 2;

        // ��ü ī�� ID Ǯ (0~15 �߿��� �����ϰ� pairCount�� �̱�)
        List<int> allCardIds = Enumerable.Range(0, 16).OrderBy(x => Random.Range(0f, 1f)).Take(pairCount).ToList();

        List<int> cardDataList = new List<int>();
        foreach (int id in allCardIds)
        {
            cardDataList.Add(id); // 1��
            cardDataList.Add(id); // ¦
        }

        // ����
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
                return 8; //��������1~3������ 8��
            case 4:
            case 5:
            case 6:
                return 12;//��������4~6�� 12��
            case 7:
            case 8:
                return 16;//��������7~8�� 16��
            case 9:
                return 8;//���罺�������� 8������ ����
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

    // ���� �������� �ر� ó��
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
