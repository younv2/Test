using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenStageButton : MonoBehaviour
{
    [SerializeField]private Button hiddenStageBtn; // HdStageJoinBtn 연결

    void Start()
    {
        // PlayerPrefs에서 바로 체크 (StageManager 없어도 됨)
        bool isUnlocked = PlayerPrefs.GetInt("clearMaxStage", 1) >= 8;
        hiddenStageBtn.gameObject.SetActive(isUnlocked);

        hiddenStageBtn.onClick.AddListener(() =>
        {
            GameManager.stage = 9;
            SceneManager.LoadScene("MainScene");
        });
    }
}
