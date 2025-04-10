using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card; //ī�� ������ ��� ����

    private int columns = 4;
    private Vector2 gap = new Vector2(1.4f, 2.0f);
    private Vector2 offset = new Vector2(-2.1f, -0.7f);

    public void SetupBoard(int stage, int[] data)
    {
        ClearBoard();

        // ������������ ��ġ ���� �ٸ���
        switch (stage)
        {
            case 1:
            case 2:
            case 3: // 8�� (4x2)
                SetBoardProperties(4, new Vector2(1.4f,2.0f), new Vector2(-2.1f,0.7f));
                break;
            case 4:
            case 5:
            case 6: // 12�� (4x3)
                SetBoardProperties(4, new Vector2(1.4f, 2.0f), new Vector2(-2.1f, 0.7f));
                break;
            case 7:
            case 8: // 16�� (4x4)
                SetBoardProperties(4, new Vector2(1.4f, 2.0f), new Vector2(-2.1f, 2.1f));
                break;
            case 9: // 8�� (4x2)
                SetBoardProperties(4, new Vector2(1.4f, 2.0f), new Vector2(-2.1f, 0.7f));
                break;
        }

        for (int i = 0; i < data.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % columns) * gap.x + offset.x;
            float y = -(i / columns) * gap.y + offset.y;
            go.transform.localPosition = new Vector2(x, y);

            go.GetComponent<Card>().Setting(data[i]);
        }
    }
    void SetBoardProperties(int colums, Vector2 gap,Vector2 offset)
    {
        this.columns = colums;
        this.gap = gap;
        this.offset = offset;
    }
    void ClearBoard() //�������� �ٲܶ� ī����� ����
    {
        for (int i = transform.childCount - 1; i >= 0; i--) //�ε��� ���� ���� ����, ���� ����
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
