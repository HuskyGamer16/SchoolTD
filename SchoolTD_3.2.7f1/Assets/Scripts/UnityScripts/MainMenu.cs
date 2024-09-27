using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(0);
    }
    public void StopGame() { 
        Application.Quit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Player"))
        {
            Debug.Log("Entered!");
        }
    }
}
