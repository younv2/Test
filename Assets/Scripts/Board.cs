using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card; //카드 프리팹 담는 변수

    private int columns = 4;
    private Vector2 gap = new Vector2(1.4f, 2.0f);
    private Vector2 offset = new Vector2(-2.1f, -0.7f);

    public void SetupBoard(int stage, int[] data)
    {
        ClearBoard();

        // 스테이지별로 배치 설정 다르게
        switch (stage)
        {
            case 1:
            case 2:
            case 3: // 8장 (4x2)
                SetBoardProperties(4, new Vector2(1.4f,2.0f), new Vector2(-2.1f,0.7f));
                break;
            case 4:
            case 5:
            case 6: // 12장 (4x3)
                SetBoardProperties(4, new Vector2(1.4f, 2.0f), new Vector2(-2.1f, 0.7f));
                break;
            case 7:
            case 8: // 16장 (4x4)
                SetBoardProperties(4, new Vector2(1.4f, 2.0f), new Vector2(-2.1f, 2.1f));
                break;
            case 9: // 8장 (4x2)
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
    void ClearBoard() //스테이지 바꿀때 카드쌓임 방지
    {
        for (int i = transform.childCount - 1; i >= 0; i--) //인덱스 꼬임 현상 방지, 역순 루프
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
