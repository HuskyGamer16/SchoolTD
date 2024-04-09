using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using MySql.Data.MySqlClient;
using System;

public class SpawnTower : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public GameObject[] Tower;
    int playerid = 1;
    public int baseDMG = 25;
    public GameObject BuyTower;
    public Vector3 place;
    public GameObject placeObj;
    int max;
    public GameObject[] AvailablePlaces;
    public List<string> OccupiedPlaces;
    List<playerTower> GetPlayerTowers;
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
        AvailablePlaces= new GameObject[max];
        playerid = GetComponent<LoginHandler>().playerid;
        AllEffects = db.SelectEffects();
        OccupiedPlaces.Clear();
        BuyTower.SetActive(false);
        Basic();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LookForGamObject(out RaycastHit hit))
                Availability(hit.collider.gameObject);
        }
    }

    private void Availability(GameObject gameObject)
    {
        bool Available = false;
        int i = 0;
        while (i < AvailablePlaces.Length && AvailablePlaces[i].gameObject != gameObject)
        {
            i++;
        }
        if (i < AvailablePlaces.Length) {
            Available = true;
        }
        if (gameObject.CompareTag("Desk") && Available) {
            Debug.Log("Asztal: " + gameObject.name);
            place = gameObject.transform.position;
            placeObj = gameObject;
        }
    }
    
    private bool LookForGamObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
    public void GetCannon() {
        Cannonbutton.enabled = true;
        BuyTower.SetActive(false);
    }
    public void Cannon()
    {
        pTowers = db.SelectEffectTower(playerid,db.GetEffectID("nothing"));
        int i = 0;
        while (place != AvailablePlaces[i].transform.position && i < AvailablePlaces.Length)
        {
            i++;
        }
        if (i < AvailablePlaces.Length)
        {
            int j =0;
            while (j < OccupiedPlaces.Count && OccupiedPlaces[j] != AvailablePlaces[i].name) { 
            j++;
            }
            if (j >= OccupiedPlaces.Count)
            {
                place.y += 5.8f;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(AvailablePlaces[i].name);
            }
            Cannonbutton.enabled = false;
            place = new Vector3(0, -100, 0);
        }
    }

    public void Water()
    { 
        pTowers = db.SelectEffectTower(playerid, db.GetEffectID("water"));
        Debug.Log("Water");
        int i = 0;
        while (place != AvailablePlaces[i].transform.position && i < AvailablePlaces.Length)
        {
            i++;
        }
        if (i < AvailablePlaces.Length)
        {
            int j = 0;
            while (j < OccupiedPlaces.Count && OccupiedPlaces[j] != AvailablePlaces[i].name)
            {
                j++;
            }
            if (j >= OccupiedPlaces.Count)
            {
                place.y += 5.8f;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(AvailablePlaces[i].name);
            }
            Waterbutton.enabled = false;
            place = new Vector3(0, -100, 0);
        }
    }
    public void Ice()
    {
        
        pTowers = db.SelectEffectTower(playerid, db.GetEffectID("ice"));
        Debug.Log("Ice");
        int i = 0;
        while (place != AvailablePlaces[i].transform.position && i < AvailablePlaces.Length) { 
            i++;
        }
        if (i <AvailablePlaces.Length ) {
            int j = 0;
            while (j < OccupiedPlaces.Count && OccupiedPlaces[j] != AvailablePlaces[i].name)
            {
                j++;
            }
            if (j >= OccupiedPlaces.Count)
            {
                place.y += 5.8f;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(AvailablePlaces[i].name);
            }
            Icebutton.enabled = false;
            place = new Vector3(0, -100, 0);
        }
    }
    public void Fire()
    {
        pTowers = db.SelectEffectTower(playerid, db.GetEffectID("fire"));
        Debug.Log("Fire");
        int i = 0;
        while (place != AvailablePlaces[i].transform.position && i < AvailablePlaces.Length) { 
            i++;
        }
        if (i < AvailablePlaces.Length)
        {
            int j = 0;
            while (j < OccupiedPlaces.Count && OccupiedPlaces[j] != AvailablePlaces[i].name)
            {
                j++;
            }
            if (j >= OccupiedPlaces.Count)
            {
                place.y += 6.8f;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(AvailablePlaces[i].name);
            }
            Firebutton.enabled = false;
            place = new Vector3(0, -100, 0);
        }
    }
    public void Electric()
    {
        pTowers = db.SelectEffectTower(playerid, db.GetEffectID("electric")); ;
        Debug.Log("Electric");
        int i = 0;
        while (place != AvailablePlaces[i].transform.position && i < AvailablePlaces.Length)
        {
            i++;
        }
        if (i < AvailablePlaces.Length)
        {
            int j = 0;
            while (j < OccupiedPlaces.Count && OccupiedPlaces[j] != AvailablePlaces[i].name)
            {
                j++;
            }
            if (j >= OccupiedPlaces.Count)
            {
                place.y += 5.8f;
                Instantiate(Tower[pTowers[0].TowerID - 1], place, Quaternion.identity);
                OccupiedPlaces.Add(AvailablePlaces[i].name);
                Electricbutton.enabled = false;
            }
            place = new Vector3(0, -100, 0);
        }
    }

    public void Basic()
    {
        GetPlayerTowers = db.SelectPlayerTower(playerid);
        Towers = db.SelectAllTower();
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
            BuyTower.SetActive(true);
        }
        else {
            Electricbutton.gameObject.SetActive(true);
            Cannonbutton.gameObject.SetActive(true);
            Waterbutton.gameObject.SetActive(true);
            Firebutton.gameObject.SetActive(true);
            Icebutton.gameObject.SetActive(true);
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
                            break;
                        case 2:
                            Firebutton.enabled = true;
                            break;
                        case 3: 
                            Waterbutton.enabled = true;
                            break;
                        case 4: 
                            Electricbutton.enabled = true;
                            break;
                        case 5:
                            Icebutton.enabled = true;
                            break;
                    }
                }
            }
        }
    }
}
