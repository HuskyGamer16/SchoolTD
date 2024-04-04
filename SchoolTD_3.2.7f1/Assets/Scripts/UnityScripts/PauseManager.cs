using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    
    public static bool IsPaused { get; private set; }   

    private void Awake()
    {
        if ( instance == null)
        {
            instance = this;
        }
    }
    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0;
    }
    public void unPause()
    {
        IsPaused = false;
        Time.timeScale = 1f;
    }
    public void WESPED() {
        IsPaused = false;
        Time.timeScale *= 10f;
    }
    public void SlowDown() {
        IsPaused = false;
        Time.timeScale *= .1f;
    }
}