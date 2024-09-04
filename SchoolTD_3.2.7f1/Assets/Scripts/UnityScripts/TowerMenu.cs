using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TowerMenu : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<playerTower> playerTowers;
    public string Menu;
    public GameObject[] Markers;
    public GameObject Camera;
    public GameObject Right;
    public GameObject Left;
    public GameObject Buy;
    public GameObject Upgrade;
    public GameObject Slider;
    public int num = 0;
    int playerid;
    public int camSpeed;
    public int currentTowerid=0;
    public bool CanBuy;
    public bool CanUpg;

    void Start()
    {
        playerid = LoginHandler.playerid;
        CanBuy = false;
        CanUpg = false;
        CanUpThis();
        CanBuyThis();
        playerTowers = db.SelectPlayerTower(playerid);
        TowerLvlCannon();
        CheckSlider();
    }
    public void CheckSlider()
    {
        Slider SliderAct = Slider.GetComponent<Slider>();
        if (currentTowerid != 0)
        {
            SliderAct.maxValue = db.GetReqXp(currentTowerid,playerid); //max xp a szinten
            SliderAct.value = db.GetLvlXp(currentTowerid, playerid); //jelen xp
        }
        else {
            SliderAct.value = 0;
        }
        if (SliderAct.value == SliderAct.maxValue)
        {
            CanUpg = true;
        }
        else {
            CanUpg = false;
            CanBuy = false;
        }
    }
    public void TowerLvlCannon()
    {
        List<playerTower> temp = new();
        switch (num) {
            case 0:
                // 1  2  3 (cannon)
                for (int i = 0; i < playerTowers.Count; i++)
                {
                    if (playerTowers[i].TowerID < 4 && playerTowers[i].TowerID >= 1) {
                        temp.Add(playerTowers[i]);
                    }
                }
                break;
            case 1: 
                // 4 5 6 7 8 (fire)
                for (int i = 0; i < playerTowers.Count; i++)
                {
                    if (playerTowers[i].TowerID < 9 && playerTowers[i].TowerID >= 4) {
                        temp.Add(playerTowers[i]);
                    }
                }
                break;
            case 2:
                // 9 10 11 12 13 (water)
                for (int i = 0; i < playerTowers.Count; i++)
                {
                    if (playerTowers[i].TowerID < 14 && playerTowers[i].TowerID >= 9)
                    {
                        temp.Add(playerTowers[i]);
                    }
                }
                break;
            case 3:
                // 14 15 16 17 18 (ice)
                for (int i = 0; i < playerTowers.Count; i++)
                {
                    if (playerTowers[i].TowerID < 19 && playerTowers[i].TowerID >= 14)
                    {
                        temp.Add(playerTowers[i]);
                    }
                }
                break;
            case 4:
                // 19 20 21 (electric)
                for (int i = 0; i < playerTowers.Count; i++)
                {
                    if (playerTowers[i].TowerID < 22 && playerTowers[i].TowerID >= 19)
                    {
                        temp.Add(playerTowers[i]);
                    }
                }
                break;
        }
        CanBuy = false;
        if (temp.Count != 0)
        {
            currentTowerid = temp[temp.Count - 1].TowerID;
            int maxlvl = db.GetMaxLvl(currentTowerid); //torony max szint
            int currentLvl = db.GetLvl(currentTowerid); //jelen szint
            if (currentLvl == maxlvl)
            {
                CanBuy = true;
                CanBuyThis();
                CheckSlider();
            }
            else
            {
                CanBuyThis();
                CheckSlider();
                CanUpThis();
            }
        }
        else
        {
            CanBuy = true;
            CanBuyThis();
            CheckSlider();
        }

    }
    public void CanBuyThis() {
        if (CanBuy)
        {
            Buy.SetActive(true);
            Upgrade.SetActive(false);
        }
        else { 
        Buy.SetActive(false);
        }
    }
    public void CanUpThis()
    {
        if (CanUpg)
        {
            Upgrade.SetActive(true);
            Buy.SetActive(false);
        }
        else
        {
           Upgrade.SetActive(false);
        }
    }
    public void BuyTower() {
        int tower = 0;
        switch (num)
        {
            case 0:
                tower = 1;
                break;
            case 1:
                tower = 4;
                break;
            case 2:
                tower = 9;
                break;
            case 3:
                tower = 14;
                break;
            case 4:
                tower = 19;
                break;
        }
        db.InsertPlayerTower(new playerTower(0,tower, playerid, 0));
        CanBuy = false;
        CanBuyThis();
        playerTowers = db.SelectPlayerTower(playerid);
        CheckSlider();
    }
    public void UpgradeTower()
    {
        db.TowerLvlUP(currentTowerid,playerid); //szint fejlesztés (szint num +1)
        CanUpg = false;
        CanUpThis();
        playerTowers = db.SelectPlayerTower(playerid);
        CheckSlider();
    }
    public void Exit()
    {
        LoginHandler.playerid = playerid; 
        SceneManager.LoadScene(Menu);
    }
    public void right()
    {
        CanBuy = false;
        int pre = num;
        num++;
        TowerLvlCannon();
        if (!Left.activeSelf)
        {
            Left.SetActive(true);
        }
        else if (num == Markers.Length - 1)
        {
            Right.SetActive(false);
        }
        position(pre);
    }
    public void left()
    {
        CanBuy = false;
        int pre = num;
        num--;
        TowerLvlCannon();
        if (!Right.activeSelf)
        {
            Right.SetActive(true);
        }
        else if (num == 0)
        {
            Left.SetActive(false);
        }
        position(pre);
    }
    public void position(int current) {
        Camera.transform.position = Vector3.MoveTowards(Markers[num].transform.position,Markers[current].transform.position,camSpeed*Time.deltaTime);
    }
}