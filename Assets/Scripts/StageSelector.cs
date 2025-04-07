using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    public Button joinBtn;
    // Start is called before the first frame update
    void Start()
    {
        joinBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }
}
