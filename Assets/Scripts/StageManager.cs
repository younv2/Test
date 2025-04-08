using System.Collections;
using System.Collections.Generic;
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

    public void StartStage(int stage) //enum���� int�� ����
    {
        ClearCards();

        int cardCount = GetCardCount(stage); //���� ���۵��ڸ��� ������ ī�尡 �ʿ����� Ȯ��.
        GenerateCards(cardCount); //�� ����ŭ ī�� ����

        GameManager.stage = (int)stage + 1; // GameManager�� ���� �������� ���� ���� (Stage1 == 0�̹Ƿ� +1)
        GameManager.instance.cardCount = cardCount; // GameManager�� ī�� �� ����
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

    void GenerateCards(int count) //�Լ��� ȣ��� �� �󸶳� ���� ī�带 ������ ���������� ����
    {
        for (int i = 0; i < count; i++) //count ��ŭ �ݺ��ؼ� �Ʒ� �ڵ带 ����
        {
            Instantiate(cardPrefab, cardParent); //�������� �� �ȿ� ���� �����ϴ� �Լ�
            //cardParent�Ʒ��� 8�� ����.
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
