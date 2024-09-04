using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckPlayerHP : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    int playerid;
    public int gradeNum;
    public float gradeSum;
    private  float gradeAvg;
    public TextMeshPro avgText;
    public GameObject LosePanel;
    public int lvlId;
    static public GameObject WinPanel;
    static public int score;
    int xp;
    private void Start()
    {
        WinPanel = GameObject.Find("Win");
        playerid = LoginHandler.playerid;
        lvlId = LevelManager.lvlId;
        gradeNum = 3;
        gradeSum = 15f;
    }
    private void Update()
    {
        WriteAvg();
        CheckHp();
    }

    private void CheckHp()
    {
        if (gradeAvg < 2.00f)
            {
                LoseCondition();
            }
        else if (gradeAvg < 2.75f)
        { 
            avgText.faceColor = Color.red;
        }
        else if (gradeAvg <= 3.50f) {
            avgText.faceColor = Color.Lerp(Color.red,Color.yellow,1);
        }
        else if (gradeAvg <= 4.25f)
        {
            avgText.faceColor = Color.yellow;
        }
        else {
            avgText.faceColor = Color.blue;
        }
    }
    private void LoseCondition()
    {
        Time.timeScale = .1f;
        LosePanel.SetActive(true);
    }
    public void ExitLose()
    {
        xp = score;
        db.TowerXPgain(playerid, xp);
        miniLVLManager.Quit();
    }
    public void QuitLose()
    {
        xp = score;
        db.TowerXPgain(playerid, xp);
        miniLVLManager.QuitToDesktop();
    }

    public static void WinCondition()
    {
        Time.timeScale = .5f;
        WinPanel.SetActive(true);
    }
    public void ExitWin()
    {
        xp = Mathf.FloorToInt(score * gradeAvg);
        db.TowerXPgain(playerid, xp);
        db.LevelCleared(playerid, score);
        miniLVLManager.Quit();
    }
    public void QuitWin()
    {
        xp = Mathf.FloorToInt(score * gradeAvg);
        db.TowerXPgain(playerid, xp);
        db.LevelCleared(playerid, score);
        miniLVLManager.QuitToDesktop();
    }
    public void WriteAvg() { 
        gradeAvg = gradeSum/gradeNum;
        avgText.text = (Mathf.Round(gradeAvg*100)/100).ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            gradeNum+=2;
            EnemyMovement EM = other.GetComponent<EnemyMovement>();
            int grade=0;
             if (((EM.Hp / EM.MaxHp) * 100) < 50)
            {
                grade = 2;
            }
            else {
                grade = 1;
            }
            gradeSum += (grade*2);
            Destroy(other.gameObject);
        }
    }
}
