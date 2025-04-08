using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageButton : MonoBehaviour
{
    public void NextStage()
    {
        GameManager.stage++;
        SceneManager.LoadScene("MainScene");
    }
}
