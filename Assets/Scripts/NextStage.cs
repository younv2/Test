using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageButton : MonoBehaviour
{
    void OnEnable()
    {
        // 8 또는 9스테이지일 경우 버튼 비활성화
        if (GameManager.stage >= 8)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void NextStage()
    {
        GameManager.stage++;
        SceneManager.LoadScene("MainScene");
    }
}
