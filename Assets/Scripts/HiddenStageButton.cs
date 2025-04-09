using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenStageButton : MonoBehaviour
{
    public Button hiddenStageBtn; // HdStageJoinBtn 연결

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

    // 혹시 UI 버튼에 연결했을 때 쓰일 수 있도록 남겨둠
    public void HdStageJoin()
    {
        GameManager.stage = 9;
        SceneManager.LoadScene("MainScene");
    }
}
