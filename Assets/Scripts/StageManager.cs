using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject cardPrefab; //ī�� ������ ��°�
    public Transform cardParent; //� ������Ʈ�� ������

    public enum StageType { Stage1, Stage2, Stage3, Stage4 }
    public StageType currentStage = StageType.Stage1; //���� ���������� ������� �����صδ� ����

    void Start()
    {
        StartStage(currentStage);
    }

    public void StartStage(StageType stage)
    {
        ClearCards();

        int cardCount = GetCardCount(stage); //���� ���۵��ڸ��� ������ ī�尡 �ʿ����� Ȯ��.
        GenerateCards(cardCount); //�� ����ŭ ī�� ����

        GameManager.stage = (int)stage + 1; // GameManager�� ���� �������� ���� ���� (Stage1 == 0�̹Ƿ� +1)
        GameManager.instance.cardCount = cardCount; // GameManager�� ī�� �� ����
    }

    int GetCardCount(StageType stage)
    {
        switch (stage)
        {
            case StageType.Stage1:
            case StageType.Stage2:
                return 8; //��������1,2������ 8��
            case StageType.Stage3:
                return 12;//��������3�� 12��
            case StageType.Stage4:
                return 16;//��������4�� 16��
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
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
    }

    // �ӽ�) ��ư���� ȣ���� �� �ְ� ���� �޼���
    public void OnClickStage1() => StartStage(StageType.Stage1);
    public void OnClickStage2() => StartStage(StageType.Stage2);
    public void OnClickStage3() => StartStage(StageType.Stage3);
    public void OnClickStage4() => StartStage(StageType.Stage4);
}
