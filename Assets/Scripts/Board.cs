using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card; //카드 프리팹 담는 변수

    public void SetupBoard(int[] data) //카드 데이터 외부에서 받고 카드 생성 및 배치
    {
        ClearBoard(); //기존 보드에 있는 카드 오브젝트 삭제

        for (int i = 0; i < data.Length; i++)
        //카드 데이터 수만큼 반복문 실행 (짝수로)
        {
            GameObject go = Instantiate(card, this.transform);
            //this.transform(부모)밑으로 자식 생성

            float x = (i % 4) * 1.4f - 2.1f; //카드 가로 위치 * 카드간격 - 좌표x
            float y = (i / 4) * 1.4f - 3.0f; //카드 세로 위치 * 카드간격 - 좌표y
            go.transform.position = new Vector2(x, y); //카드 위치 계산한 값으로 설정

            go.GetComponent<Card>().Setting(data[i]); //setting 함수를 통해 카드의 내용을 설정.
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
