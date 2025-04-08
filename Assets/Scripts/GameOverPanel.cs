using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverPanel : MonoBehaviour
{
    public GameObject cards;
    public GameObject successTxt; 
    public GameObject failTxt;

    public Button nextStageBtn;

    public GameObject star;

    public void ShowResult(bool isSuccess, int starCount = 0)
    {
        nextStageBtn.interactable = false;
        successTxt.SetActive(isSuccess);
        failTxt.SetActive(!isSuccess);
        if (isSuccess)
        {
            cards.SetActive(true);
            star.GetComponent<Star>().Setting(starCount);
            star.SetActive(true);
            if(starCount == 3) {
                nextStageBtn.interactable = true;
            }
        }

        Debug.Log("[GameOverpanel] isSuccess: " + isSuccess);
        Debug.Log("[GameOverPanel] starCount: " + starCount);
    }
}

