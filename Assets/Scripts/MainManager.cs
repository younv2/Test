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
    private void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("StageSelectionScene", LoadSceneMode.Additive);
        });
        settingBtn.onClick.AddListener(() =>
        {

        });
        collectionBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("CollectionScene");
        });
    }
}
