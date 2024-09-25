using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnTower : MonoBehaviour
{
    public SpawnTower instance; 
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public GameObject[] Tower;
    int playerid = 1;
    public int baseDMG = 25;
    public GameObject BuyTower;
    public GameObject Menu;
    public Vector3 place;
    public GameObject placeObj;
    public GameObject TowerDownBtn;
    int max;
    public GameObject[] AllPlaces;
    public GameObject Settings;
    public List<playerTower> pTowers;
    public List<int> UsedTowerIDs;
    public List<GameObject> OccupiedPlaces;
    List<playerTower> GetPlayerTowers;
    
    List<TotalTower> Towers;
    

    int level;
    public UnityEngine.UI.Button Firebutton;
    public UnityEngine.UI.Button Waterbutton;
    public UnityEngine.UI.Button Icebutton;
    public UnityEngine.UI.Button Electricbutton;
    public UnityEngine.UI.Button Cannonbutton;
    bool paused;
    
    void Start()
    {
        Time.timeScale = 1f;
        UsedTowerIDs = new();
        paused = false;
        level = LevelManager.lvlnum + 1; //Itt ennek egy norm�lis megold�st k�ne csin�lni majd valamikor :shrug:
        // Elvileg ez egy normalis megoldas, de dunno, it works
        max = level*2+3;
        GetPlayerTowers = db.SelectPlayerTower(playerid);
        Towers = db.SelectAllTower();
        AllPlaces = GameObject.FindGameObjectsWithTag("Bok");
        playerid = LoginHandler.playerid;
        OccupiedPlaces.Clear();
        if (Menu.activeSelf)
        {
            Electricbutton.enabled = false;
            Cannonbutton.enabled = false;
            Waterbutton.enabled = false;
            Firebutton.enabled = false;
            Icebutton.enabled = false;
            Basic();
        }
        TowerDownBtn.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (!paused){
                Time.timeScale = 0;
                Settings.SetActive(true);
            }
            else { 
                Time.timeScale = 1f;
                Settings.SetActive(false);
            }
            paused = !paused;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (LookForGamObject(out RaycastHit hit))
                Availability(hit.collider.gameObject);
        }
    }
    
    private void Availability(GameObject gameObject)
    {
        if (gameObject.CompareTag("Bok")) {
            TowerSelect(gameObject);
            place = gameObject.transform.position;
            placeObj = gameObject;
        }
        else
        {
            if (!gameObject.TryGetComponent<Button>(out Button btn)) {
                if (Menu.activeSelf)
                {
                    Menu.SetActive(false);
                }
            }
        }
    }

    private void TowerSelect(GameObject obj)
    {
        if (Time.timeScale != 0)
        {
            if (placeObj == obj)
            {
                if (Menu.activeSelf)
                {
                    Menu.SetActive(false);
                }
                else
                {
                    Menu.SetActive(true);
                }
            }
            else
            {
                if (!Menu.activeSelf)
                {
                    Menu.SetActive(true);
                }
            }
        }
    }

    public static effects GiveEffect(int num) {
        effects eff = db.GetAllEffects()[num];
        return eff;
    }
    private bool LookForGamObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
    public void PlaceTower(string param)
    {
        float yPos = 9.8f;
        pTowers = new List<playerTower>();
        switch (param)
        {
            case "cannon":
                pTowers = db.SelectPlayerTower(playerid, "CANNON");
                //Debug.Log(db.SelectTower(pTowers[0].TowerID)[0]);
                Tower[pTowers[0].TowerID - 1].GetComponent<shootTurretBasic>().towerid = db.SelectTower(pTowers[0].TowerID)[0].Id;
                Tower[pTowers[0].TowerID - 1].GetComponent<shootTurretBasic>().towerdmg = db.SelectTower(pTowers[0].TowerID)[0].Dmg;
                //Debug.Log(Tower[pTowers[0].TowerID - 1].GetComponent<shootTurretBasic>().tower.Dmg);
                break;
            case "fire":
                pTowers = db.SelectPlayerTower(playerid, "FIRE");
                //for (int i = 0; i < pTowers.Count; i++)
                //{
                //    Debug.Log(pTowers[i].TowerID);
                //}
                Tower[pTowers[0].TowerID - 1].GetComponentInChildren<ShootTurretAriaDmg>().towerid = db.SelectTower(pTowers[0].TowerID)[0].Id;
                Tower[pTowers[0].TowerID - 1].GetComponentInChildren<ShootTurretAriaDmg>().towerdmg = db.SelectTower(pTowers[0].TowerID)[0].Dmg;
                yPos += 1.5f;
                break;
            case "water":
                pTowers = db.SelectPlayerTower(playerid, "WATER");
                Tower[pTowers[0].TowerID - 1].GetComponentInChildren<ShootTowerWater>().towerid = db.SelectTower(pTowers[0].TowerID)[0].Id;
                Tower[pTowers[0].TowerID - 1].GetComponentInChildren<ShootTowerWater>().towerdmg = db.SelectTower(pTowers[0].TowerID)[0].Dmg;
                break;
            case "ice":
                pTowers = db.SelectPlayerTower(playerid,"ICE");
                Tower[pTowers[0].TowerID - 1].GetComponent<ShootTurretAOEDmg>().towerid = db.SelectTower(pTowers[0].TowerID)[0].Id;
                Tower[pTowers[0].TowerID - 1].GetComponent<ShootTurretAOEDmg>().towerdmg = db.SelectTower(pTowers[0].TowerID)[0].Dmg;
                break;
            case "electric":
                pTowers = db.SelectPlayerTower(playerid, "ELECTRIC");
                Tower[pTowers[0].TowerID - 1].GetComponent<ShootTurretElectric>().towerid = db.SelectTower(pTowers[0].TowerID)[0].Id;
                Tower[pTowers[0].TowerID - 1].GetComponent<ShootTurretElectric>().towerdmg = db.SelectTower(pTowers[0].TowerID)[0].Dmg;
                break;
        }
        if (OccupiedPlaces.Count < max)
        {
            int j = 0;
            while (j < UsedTowerIDs.Count && UsedTowerIDs[j] != pTowers[0].TowerID)
            {
                j++;
            }
            Debug.Log($"J: {j}");
            if (j >= UsedTowerIDs.Count) { 
                place.y = yPos;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(placeObj);
                UsedTowerIDs.Add(pTowers[0].TowerID);
                place = new Vector3(0, -100, 0);
                placeObj.SetActive(false);
                Menu.SetActive(false);
                Basic();
            }
        }
    }
    #region Towers
    public void Cannon()
    {
        PlaceTower("cannon");
    }

    public void Water()
    { 
        PlaceTower("water");
    }
    public void Ice()
    {
        PlaceTower("ice");
    }
    public void Fire()
    {
        PlaceTower("fire");
    }
    public void Electric()
    {
        PlaceTower("electric");
    }
    #endregion
    public void Basic()
    {
        Debug.Log("Basic commense");
        if (GetPlayerTowers.Count != 0 && GetPlayerTowers != null)
        {        
            //Debug.Log($"{GetPlayerTowers.Count}");
            //GetPlayerTowers.Count = 5
            //Towers.Count = 21
            //WHYISNOWORK
            for (int i = 0; i < GetPlayerTowers.Count; i++)
            {
                //iterating getPlayerTowers[i]
                int j = 0;
                while (j < Towers.Count && GetPlayerTowers[i].TowerID != Towers[j].Id) { 
                    j++;
                }
                if (j < Towers.Count) {
                    //Debug.Log($"Basic() Towername: {Towers[j].Name}");
                    //If there is a type of tower in gPT, unlock its type btn
                    switch (Towers[j].Name) {
                        case "CANNON":
                            Cannonbutton.enabled = true;
                            break;
                        case "FIRE":
                            Firebutton.enabled = true;
                            break;
                        case "WATER": 
                            Waterbutton.enabled = true;
                            break;
                        case "ELECTRIC": 
                            Electricbutton.enabled = true;
                            break;
                        case "ICE":
                            Icebutton.enabled = true;
                            break;
                    }
                }
            }
        }
    }
}
