using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    int playerid;
    bool test;
    List<clearedLevel> AllLevels;
    public GameObject[] Tower;
    public string Level1;
    public string Level2;
    public string Level3;
    public string Level4;
    public string Level5;
    public string Level6;
    public string TowerSelect;
    public string Exit;
    public GameObject tolvl2;
    public GameObject tolvl3;
    public GameObject tolvl4;
    public GameObject tolvl5;
    public GameObject tolvl6;
    public GameObject[] Doors;
    public static int lvlId;
    void Start()
    {
        playerid = LoginHandler.playerid;
        test = true;
    }
    public void CheckLevel()
    {
        tolvl2.SetActive(true);
        tolvl3.SetActive(true);
        tolvl4.SetActive(true);
        tolvl5.SetActive(true);
        tolvl6.SetActive(true);
        AllLevels = db.SelectLevels(playerid);
        switch (AllLevels.Count)
        {
            case 1:
                Debug.Log("max lvl2");
                if (!tolvl2.activeSelf)
                {
                    tolvl2.SetActive(true);
                }
                tolvl3.SetActive(false);

                break;
            case 2:
                Debug.Log("max lvl3");
                if (!tolvl2.activeSelf)
                {
                    tolvl2.SetActive(true);
                }
                if (!tolvl3.activeSelf)
                {
                    tolvl3.SetActive(true);
                }
                tolvl4.SetActive(false);
                break;
            case 3:
                Debug.Log("max lvl4");
                if (!tolvl2.activeSelf)
                {
                    tolvl2.SetActive(true);
                }
                if (!tolvl3.activeSelf)
                {
                    tolvl3.SetActive(true);
                }
                if (!tolvl4.activeSelf)
                {
                    tolvl4.SetActive(true);
                }
                tolvl5.SetActive(false);
                break;
            case 4:
                Debug.Log("max lvl5");
                if (!tolvl2.activeSelf)
                {
                    tolvl2.SetActive(true);
                }
                if (!tolvl3.activeSelf)
                {
                    tolvl3.SetActive(true);
                }
                if (!tolvl4.activeSelf)
                {
                    tolvl4.SetActive(true);
                }
                if (!tolvl5.activeSelf)
                {
                    tolvl5.SetActive(true);
                }
                tolvl6.SetActive(false);
                break;
            case 5:
                Debug.Log("max lvl6");
                if (!tolvl2.activeSelf)
                {
                    tolvl2.SetActive(true);
                }
                if (!tolvl3.activeSelf)
                {
                    tolvl3.SetActive(true);
                }
                if (!tolvl4.activeSelf)
                {
                    tolvl4.SetActive(true);
                }
                if (!tolvl5.activeSelf)
                {
                    tolvl5.SetActive(true);
                }
                if (!tolvl6.activeSelf)
                {
                    tolvl6.SetActive(true);
                }
                break;
            default:
                Debug.Log("Nincs");
                tolvl2.SetActive(false);
                break;
        }
    }
    void Update()
    {
        if (test)
        {
            CheckLevel();
            test = !test;
        }
    }
    public void lvl1()
    {
        lvlId = 1;
        SceneManager.LoadScene(Level1);
    }
    public void lvl2()
    {
        lvlId = 2;
        SceneManager.LoadScene(Level2);
    }
    public void lvl3()
    {
        lvlId = 3;
        SceneManager.LoadScene(Level3);
    }
    public void lvl4()
    {
        lvlId = 4;
        SceneManager.LoadScene(Level4);
    }
    public void lvl5()
    {
        lvlId = 5;
        SceneManager.LoadScene(Level5);
    }
    public void lvl6()
    {
        lvlId = 6;
        SceneManager.LoadScene(Level6);
    }
    public void TowerShop()
    {
        SceneManager.LoadScene(TowerSelect);
    }
    public void Quit()
    {
        LoginHandler.playerid = playerid;
        SceneManager.LoadScene(Exit);
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
