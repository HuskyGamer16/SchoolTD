using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniLVLManager : MonoBehaviour
{
    static public void Quit()
    {
        SceneManager.LoadScene("Level_Select");
        LevelManager.playerid = LoginHandler.playerid;
    }
   static public void QuitToDesktop()
    {
        Application.Quit();
    }
}
