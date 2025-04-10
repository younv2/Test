using UnityEngine;

public class CardSwitch : MonoBehaviour
{
    [SerializeField]private GameObject back;
    [SerializeField]private GameObject[] profiles;

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
            profileIndex = (profileIndex + 1) % profiles.Length;
        }
        else
        {
            ShowBack();
        }

        isBack = !isBack;
    }

    void ShowBack()
    {
        back.SetActive(true);
        foreach (var profile in profiles)
            profile.SetActive(false);
    }

    void ShowProfile(int index)
    {
        back.SetActive(false);
        for (int i = 0; i < profiles.Length; i++)
            profiles[i].SetActive(i == index);
    }
}
