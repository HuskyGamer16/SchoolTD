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
    public List<GameObject> OccupiedPlaces;
    List<playerTower> GetPlayerTowers;
    List<playerTower> UsedTowers;
    List<TotalTower> Towers;
    List<playerTower> pTowers;
    List<effects> AllEffects;
    levels level;
    public UnityEngine.UI.Button Firebutton;
    public UnityEngine.UI.Button Waterbutton;
    public UnityEngine.UI.Button Icebutton;
    public UnityEngine.UI.Button Electricbutton;
    public UnityEngine.UI.Button Cannonbutton;
    
    void Start()
    {
        level = db.SelectLevel(1)[0];
        max = level.MaxBuildables;
        GetPlayerTowers = db.SelectPlayerTower(playerid);
        Towers = db.SelectAllTower();
        AllPlaces = GameObject.FindGameObjectsWithTag("Bok");
        //playerid = LoginHandler.playerid;
        AllEffects = db.SelectEffects();
        OccupiedPlaces.Clear();
        //BuyTower.SetActive(false);
        if (Menu.activeSelf)
        {
            Basic();
        }
        TowerDownBtn.SetActive(false);
    }

    void Update()
    {
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

    public static effects GiveEffect(int effectid) {
        effects eff = db.SelectEffect(effectid)[0];
        return eff;
    }
    private bool LookForGamObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
    public void PlaceTower(string param)
    {
        float y = 0;
        switch (param)
        {
            case "cannon":
                pTowers = db.SelectEffectTower(playerid, db.GetEffectID("nothing"));
                y = 9.8f;
                break;
            case "fire":
                pTowers = db.SelectEffectTower(playerid, db.GetEffectID(param));
                y = 11.3f;
                break;
            case "water":
                pTowers = db.SelectEffectTower(playerid, db.GetEffectID(param));
                y = 9.8f;
                break;
            case "ice":
                pTowers = db.SelectEffectTower(playerid, db.GetEffectID(param));
                y = 9.8f;
                break;
            case "electric":
                pTowers = db.SelectEffectTower(playerid, db.GetEffectID(param));
                y = 9.8f;
                break;
        }
        if (OccupiedPlaces.Count <= max)
        {
            place.y = y;
            Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
            Basic();
            OccupiedPlaces.Add(placeObj);
            place = new Vector3(0, -100, 0);
            placeObj.SetActive(false);
            Menu.SetActive(false);
        }
    }
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

    public void Basic()
    {
        if (GetPlayerTowers.Count == 0 || GetPlayerTowers == null)
        {
            Electricbutton.enabled = false;
            Cannonbutton.enabled = false;
            Waterbutton.enabled = false;
            Firebutton.enabled = false;
            Icebutton.enabled = false;
            Electricbutton.gameObject.SetActive(false);
            Cannonbutton.gameObject.SetActive(false);
            Waterbutton.gameObject.SetActive(false);
            Firebutton.gameObject.SetActive(false);
            Icebutton.gameObject.SetActive(false);
        }
        else {
            for (int i = 0; i < GetPlayerTowers.Count; i++)
            {
                int j = 0;
                while (j < Towers.Count && GetPlayerTowers[i].TowerID != Towers[j].Id) { 
                j++;
                }
                if (j < Towers.Count) {
                    switch (Towers[j].EffectID) {
                        case 1:
                            Cannonbutton.enabled = true;
                            Cannonbutton.gameObject.SetActive(true);
                            break;
                        case 2:
                            Firebutton.enabled = true;
                            Firebutton.gameObject.SetActive(true);
                            break;
                        case 3: 
                            Waterbutton.enabled = true;
                            Waterbutton.gameObject.SetActive(true);
                            break;
                        case 4: 
                            Electricbutton.enabled = true;
                            Electricbutton.gameObject.SetActive(true);
                            break;
                        case 5:
                            Icebutton.enabled = true;
                            Icebutton.gameObject.SetActive(true);
                            break;
                    }
                }
            }
        }
    }
}
