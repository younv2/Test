using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectionButton : MonoBehaviour
{
   public void ToCollection()
    {
        SceneManager.LoadScene("CollectionScene");
    }
}
