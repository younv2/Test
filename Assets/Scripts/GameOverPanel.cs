using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject subCard;
    public GameObject undisCard;
    public GameObject successTxt;
    public GameObject starImage1;
    public GameObject starImage2;
    public GameObject starImage3;
    public GameObject starImage4;
    public GameObject starImage5;
    public GameObject failTxt;

    public Button nextStageBtn;

    [SerializeField] private StarAnimator[] starAnimators; // StarImage1 ~ StarImage5�� ���� ��ũ��Ʈ��

    public void ShowResult(bool isSuccess, int starCount = 0)
    {
        subCard.SetActive(false);
        undisCard.SetActive(true);
        nextStageBtn.interactable = false;
        starImage1.SetActive(false);
        starImage2.SetActive(false);
        starImage3.SetActive(false);
        starImage4.SetActive(false);
        starImage5.SetActive(false);
        successTxt.SetActive(isSuccess);
        failTxt.SetActive(!isSuccess);

        if (isSuccess)
        {
            subCard.SetActive(true);
            undisCard.SetActive(false);
            nextStageBtn.interactable = (starCount == 3);

            ShowStars(starCount);
        }

        Debug.Log("[GameOverpanel] isSuccess: " + isSuccess);
        Debug.Log("[GameOverPanel] starCount: " + starCount);
    }

    public void ShowStars(int starCount)
    {
        // ��� �� ��Ȱ��ȭ
        foreach (var animator in starAnimators)
        {
            animator.gameObject.SetActive(false);
        }

        // ���� ��: �ִϸ��̼� ��� (0~2)
        for (int i = 0; i < starCount; i++)
        {
            starAnimators[i].PlayPop();
        }

        // ��ο� ��: 3 - starCount ��ŭ Ȱ��ȭ (��: starCount 2�� ��ο 1��)
        for (int i = starCount; i < 3; i++)
        {
            int darkIndex = 3 + (i - starCount); // 3, 4 �� ����
            starAnimators[darkIndex].gameObject.SetActive(true);
        }
    }
}
