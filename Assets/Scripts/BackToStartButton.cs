using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToStartButton : MonoBehaviour
{
    public void BackToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
