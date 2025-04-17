using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageSelector : MonoBehaviour
{
    public Button joinBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button hiddenStageButton;
    public StageManager stageManager;

    public Text stageTxt;

    public int maxStage = 8;

    public int stage = 1;

    int activeMaxStage;



    void Start()
    {
        //Debug.Log("activeMaxStage = " + activeMaxStage);


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
            int maxSelectableStage = Mathf.Min(activeMaxStage, 8); // ���� 8 �̻� �� ����
            if (stage < maxSelectableStage)
                stage++;

            stage = Mathf.Clamp(stage, 1, 8); // ����ڵ�
            stageTxt.text = stage.ToString();
        });

        stageTxt.text = stage.ToString();
    }
}
