using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    static public int playerid;
    bool test;
    int AllLevels;
    public GameObject[] Tower;
    static public int lvlnum;
    public string TowerSelect;
    public string Exit;
    public GameObject tolvl2;
    public GameObject tolvl3;
    public GameObject tolvl4;
    public GameObject tolvl5;
    public GameObject tolvl6;
    public static int lvlId;
    void Start()
    {
        playerid = LoginHandler.playerid;
        test = true;
        lvlnum = 0;
        Debug.Log(playerid);
    }
    public void CheckLevel()
    {
        tolvl2.SetActive(true);
        tolvl3.SetActive(true);
        tolvl4.SetActive(true);
        tolvl5.SetActive(true);
        tolvl6.SetActive(true);
        AllLevels = db.GetLevels(playerid);
        switch (AllLevels)
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
    public void AddLvlNum() {
    if(lvlnum<5)
        lvlnum++;
    }
    public void SubLvlNum() {
        if(lvlnum>1)
        lvlnum--;
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
    public void Level()
    {
        //Debug.Log("Lvl" + (lvlnum + 1));
        SceneManager.LoadScene("Lvl" + (lvlnum + 1));
    }
}
