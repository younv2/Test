using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour
{
    public void Close()
    {
        SceneManager.UnloadSceneAsync("StageSelectionScene");
    }
}
