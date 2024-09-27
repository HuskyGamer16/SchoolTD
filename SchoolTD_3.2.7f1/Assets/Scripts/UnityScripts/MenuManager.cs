using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    void OpenMainMenu() { 
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    void OpenSettingsMenu() {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    void CloseAllMenus() {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    private void Start()
    {
        CloseAllMenus();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!PauseManager.IsPaused)
            {
                PauseManager.instance.Pause();
                OpenMainMenu();
            }
            else
            {
                PauseManager.instance.unPause();
                CloseAllMenus();
            }
        }
    }
}