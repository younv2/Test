using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Button startBtn;
    public Button settingBtn;
    public Button collectionBtn;
    public SettingPopup settingPopup;
    private void Awake()
    {
        startBtn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("StageSelectionScene", LoadSceneMode.Additive);
        });
        settingBtn.onClick.AddListener(() =>
        {
            settingPopup.gameObject.SetActive(true);
        });
        collectionBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("CollectionScene");
        });
    }
    private void Start()
    {
        SoundManager.instance.PlaySound(SoundType.BGM, "BGM", true);
    }
}
