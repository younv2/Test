using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card; //ī�� ������ ��� ����

    public void SetupBoard(int[] data) //ī�� ������ �ܺο��� �ް� ī�� ���� �� ��ġ
    {
        ClearBoard(); //���� ���忡 �ִ� ī�� ������Ʈ ����

        for (int i = 0; i < data.Length; i++)
        //ī�� ������ ����ŭ �ݺ��� ���� (¦����)
        {
            GameObject go = Instantiate(card, this.transform);
            //this.transform(�θ�)������ �ڽ� ����

            float x = (i % 4) * 1.4f - 2.1f; //ī�� ���� ��ġ * ī�尣�� - ��ǥx
            float y = (i / 4) * 1.4f - 3.0f; //ī�� ���� ��ġ * ī�尣�� - ��ǥy
            go.transform.position = new Vector2(x, y); //ī�� ��ġ ����� ������ ����

            go.GetComponent<Card>().Setting(data[i]); //setting �Լ��� ���� ī���� ������ ����.
        }
    }

    void ClearBoard() //�������� �ٲܶ� ī����� ����
    {
        for (int i = transform.childCount - 1; i >= 0; i--) //�ε��� ���� ���� ����, ���� ����
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
