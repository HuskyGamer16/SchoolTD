using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniLVLManager : MonoBehaviour
{
    static public void Quit()
    {
        SceneManager.LoadScene("Level_Select");
    }
   static public void QuitToDesktop()
    {
        Application.Quit();
    }
}
