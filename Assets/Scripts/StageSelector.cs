using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageSelector : MonoBehaviour
{
    [SerializeField] private Button joinBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button hiddenStageButton;

    public Text stageTxt;

    public int maxStage = 8;

    public int stage = 1;

    private int activeMaxStage;



    void Start()
    {
        activeMaxStage = PlayerPrefs.GetInt("clearMaxStage");
        if(activeMaxStage == 0)
        {
            activeMaxStage = 1;
        }

        if (hiddenStageButton != null)
        {
            bool isUnlocked = activeMaxStage >= 8;
            hiddenStageButton.gameObject.SetActive(isUnlocked);
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
            int maxSelectableStage = Mathf.Min(activeMaxStage, 8); // 절대 8 이상 못 가게
            if (stage < maxSelectableStage)
                stage++;

            stage = Mathf.Clamp(stage, 1, 8); // 방어코드
            stageTxt.text = stage.ToString();
        });

        stageTxt.text = stage.ToString();
    }
}
