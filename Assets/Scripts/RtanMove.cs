using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RtanMove : MonoBehaviour
{
    public Transform character; // 캐릭터 오브젝트
    public Image roadmapImage; // 로드맵 이미지
    public StageSelector stageSelector; // 현재 선택 중인 스테이지 정보 참조

    private Vector2[] stagePositions = new Vector2[8];
    private Sprite[] roadmapSprites;

    void Start()
    {
        // 로드맵 이미지 배열 초기화
        roadmapSprites = new Sprite[8];

        for (int i = 0; i < 8; i++)
        {
            roadmapSprites[i] = Resources.Load<Sprite>($"Images/Roadmap/Roadmap{i + 1}");
            if (roadmapSprites[i] == null)
            {
                Debug.LogError($"로드맵 이미지 로딩 실패: Roadmap{i + 1}");
            }
        }

        // 캐릭터의 각 스테이지 위치 설정
        stagePositions[0] = new Vector2(-239f, 27f); // 1스테이지
        stagePositions[1] = new Vector2(-198f, 48f); // 2
        stagePositions[2] = new Vector2(-8f, 48f);   // 3
        stagePositions[3] = new Vector2(187f, -22f); // 4
        stagePositions[4] = new Vector2(-8f, -92f);  // 5
        stagePositions[5] = new Vector2(-208f, -156f); // 6
        stagePositions[6] = new Vector2(-8f, -225f); // 7
        stagePositions[7] = new Vector2(178f, -225f); // 8
    }

    void Update()
    {
        if (stageSelector == null || character == null || roadmapImage == null) return;

        int selectedStage = stageSelector.stage;
        if (selectedStage < 1 || selectedStage > 8) return;

        // 캐릭터 이동
        Vector2 targetPos = stagePositions[selectedStage - 1];
        RectTransform rect = character.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, Time.deltaTime * 5f);

        // 캐릭터 방향 설정:  → 왼쪽 / 나머지 → 오른쪽
        if (selectedStage == 5 || selectedStage == 6)
        {
            character.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            character.localScale = new Vector3(1, 1, 1);
        }

        // 로드맵 이미지 변경
        if (roadmapImage.sprite != roadmapSprites[selectedStage - 1])
        {
            roadmapImage.sprite = roadmapSprites[selectedStage - 1];
        }
    }
}
