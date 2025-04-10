using UnityEngine;

public class CardSwitch : MonoBehaviour
{
    public GameObject Back;
    public GameObject[] Profiles;

    private int profileIndex = 0;
    private bool isBack = true;

    void Start()
    {
        ShowBack();
    }

    // 애니메이션 중간에 호출됨 (Y축 90도쯤)
    public void SwitchSides()
    {
        if (isBack)
        {
            ShowProfile(profileIndex);
            profileIndex = (profileIndex + 1) % Profiles.Length;
        }
        else
        {
            ShowBack();
        }

        isBack = !isBack;
    }

    void ShowBack()
    {
        Back.SetActive(true);
        foreach (var profile in Profiles)
            profile.SetActive(false);
    }

    void ShowProfile(int index)
    {
        Back.SetActive(false);
        for (int i = 0; i < Profiles.Length; i++)
            Profiles[i].SetActive(i == index);
    }
}
