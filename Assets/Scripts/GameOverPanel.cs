using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject subCard;
    [SerializeField] private GameObject undisCard;
    [SerializeField] private Text resultTxt;
    [SerializeField] private GameObject[] successStars;
    [SerializeField] private GameObject[] failedStars;

    [SerializeField] private Button nextStageBtn;

    public void ShowResult(bool isSuccess, int starCount = 0)
    {
        subCard.SetActive(isSuccess);
        undisCard.SetActive(!isSuccess);
        foreach(var star in successStars)
        {
            star.SetActive(false);
        }
        foreach (var star in failedStars)
        {
            star.SetActive(false);
        }
        nextStageBtn.interactable = (starCount == 3) ? true : false;
        SetResultMsg(isSuccess);
        if(isSuccess)
            ShowStars(starCount);
    }

    public void ShowStars(int starCount)
    {

        // 밝은 별: 애니메이션 재생 (0~2)
        for (int i = 0; i < starCount; i++)
        {
            successStars[i].SetActive(true);
        }

        // (예: starCount 2면 어두운별 1개)
        for (int i = 0; i < 3-starCount ; i++)
        {
            failedStars[i].SetActive(true);
        }
    }
    public void SetResultMsg(bool isSuccess)
    {
        resultTxt.gameObject.SetActive(true);
        resultTxt.text = isSuccess ? Global.StrMsg.SUCCESS_MSG : Global.StrMsg.FAILED_MSG;
        resultTxt.color = isSuccess ? new Color(50 / 255f,1,0f) : new Color(1f,0,0);
    }
}
