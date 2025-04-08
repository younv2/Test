using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card; //카드 프리팹 담는 변수


    public void SetupBoard(int stage, int[] data)
    {
        ClearBoard();

        int columns = 4;
        float gapX = 1.4f;
        float gapY = 2.0f;
        float offsetX = -2.1f;
        float offsetY = 0.7f;

        // 스테이지별로 배치 설정 다르게
        switch (stage)
        {
            case 1:
            case 2:
            case 3: // 8장 (4x2)
                columns = 4;
                gapX = 1.4f;
                gapY = 2.0f;
                offsetX = -2.1f;
                offsetY = 0.7f;
                break;
            case 4:
            case 5:
            case 6: // 12장 (4x3)
                columns = 4;
                gapX = 1.4f;
                gapY = 2.0f;
                offsetX = -2.1f;
                offsetY = 0.7f;
                break;
            case 7:
            case 8: // 16장 (4x4)
                columns = 4;
                gapX = 1.4f;
                gapY = 2.0f;
                offsetX = -2.1f;
                offsetY = 2.1f;
                break;
        }

        for (int i = 0; i < data.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % columns) * gapX + offsetX;
            float y = -(i / columns) * gapY + offsetY;
            go.transform.localPosition = new Vector2(x, y);

            go.GetComponent<Card>().Setting(data[i]);
        }
    }


    void ClearBoard() //스테이지 바꿀때 카드쌓임 방지
    {
        for (int i = transform.childCount - 1; i >= 0; i--) //인덱스 꼬임 현상 방지, 역순 루프
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
