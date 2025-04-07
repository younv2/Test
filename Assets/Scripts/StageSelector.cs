using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageSelector : MonoBehaviour
{
    public Button joinBtn;
    public Button leftBtn;
    public Button rightBtn;

    public Text stageTxt;

    public int maxStage = 8;

    public int stage = 1;

    int activeMaxStage;
    // Start is called before the first frame update
    
    void Start()
    {
        activeMaxStage = PlayerPrefs.GetInt("clearMaxStage");
        if(activeMaxStage == 0)
        {
            activeMaxStage = 1;
        }
        joinBtn.onClick.AddListener(() =>
        {
            GameManager.stage = stage;
            SceneManager.LoadScene("MainScene");
        });

        leftBtn.onClick.AddListener(() =>
        {
            if(stage <= 1)
            {
                return;
            }
            stage--;
            stageTxt.text = stage.ToString();
        });

        rightBtn.onClick.AddListener(() =>
        {
            if (stage >= activeMaxStage)
                return;
            stage++;
            stageTxt.text = stage.ToString();
        });
    }
}
