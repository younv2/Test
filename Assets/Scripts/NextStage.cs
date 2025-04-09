using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageButton : MonoBehaviour
{
    void OnEnable()
    {
        // 8 �Ǵ� 9���������� ��� ��ư ��Ȱ��ȭ
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
