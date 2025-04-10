using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenStageButton : MonoBehaviour
{
    [SerializeField]private Button hiddenStageBtn; // HdStageJoinBtn ����

    void Start()
    {
        // PlayerPrefs���� �ٷ� üũ (StageManager ��� ��)
        bool isUnlocked = PlayerPrefs.GetInt("clearMaxStage", 1) >= 8;
        hiddenStageBtn.gameObject.SetActive(isUnlocked);

        hiddenStageBtn.onClick.AddListener(() =>
        {
            GameManager.stage = 9;
            SceneManager.LoadScene("MainScene");
        });
    }
}
