using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using System.Timers;
using TMPro;
using System;
using Random = UnityEngine.Random;
public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] myPrefabs = new GameObject[9];
    public float timeToSpawn;
    public float spawnCooldown;
    public GameObject StartPoint;
    public List<GameObject> EffectPrefabs;
    bool waveFinished = false;
    public TextMeshPro score;
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<effects> GetEffects = new();
    List<origami> GetEnemies = new();
    List<waves> GetWaves = new();
    List<levels> GetLevel = new();
    private float tillWaveSpawn = 0;
    int lvlId;
    int max;
    int amount = 0;
    int currentWave = 0;
    public int[] enemyWeight = new int[] {80,20,20};
    public int[] waves;
    void Start()
    {
        lvlId = (LevelManager.lvlnum+1);
        spawnCooldown = timeToSpawn;
        Enemies();
    }
    private void Update()
    {
        try
        {
            if (currentWave < waves.Length)
            {
                if (!waveFinished && tillWaveSpawn <= 0)
                {
                    if (!waveFinished)
                    {
                        Spawn(max);
                    }
                    if (amount >= max)
                    {
                        waveFinished = true;
                        tillWaveSpawn = 10;
                        currentWave++;
                        Enemies();
                        amount = 0;
                    }
                }
                if (tillWaveSpawn > 0)
                {
                    tillWaveSpawn -= Time.deltaTime;
                    waveFinished = false;
                }
            }
        }
        catch (Exception) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            while (enemies != null) { 
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
            CheckPlayerHP.WinCondition();
        }
    }
    public void Spawn(int max)
    {
        if (amount <= max)
        {
            spawnCooldown -= Time.deltaTime;
        }
        if (spawnCooldown <= 0)
        {
            Instantiate(RandomEnemy(), StartPoint.transform.position, Quaternion.Euler(new Vector3(-90, -180, 0)));
            spawnCooldown = timeToSpawn;
            amount++;
        }
    }
    public GameObject RandomEnemy()
    {
        int Weight = enemyWeight[0] + enemyWeight[1] + enemyWeight[2];
        int r = Random.Range(0, Weight+1);
        int a;
        if (r< enemyWeight[0])
        {
            a = 0;
        }
        else if (r < enemyWeight[0] + enemyWeight[1])
        {
            a = 1;
        }
        else
        {
            a = 2;
        }
        int r2 = Random.Range(0,101);
        if (r2 >= 20 && r2 <80)
        {
            a += 3;
        }
        else if (r2 >= 80)
        {
            a += 6;
        }
        EnemyMovement Em = myPrefabs[a].GetComponent<EnemyMovement>();
        switch (a % 3)
        {
            case 0:
                Em.Hp = GetEnemies[0].BaseHP;
                Em.Def = GetEnemies[0].BaseDef;
                Em.SPD = GetEnemies[0].BaseSpeed;
                Em.EXP = GetEnemies[0].EXP;
                //Em.weakness = db.SelectEffect((GetEnemies[0].EffectID))[0];
                break;
            case 1:
                Em.Hp = GetEnemies[1].BaseHP;
                Em.Def = GetEnemies[1].BaseDef;
                Em.SPD = GetEnemies[1].BaseSpeed;
                Em.EXP = GetEnemies[1].EXP;
                //Em.weakness = db.SelectEffect((GetEnemies[1].EffectID))[0];
                break;
            case 2:
                Em.Hp = GetEnemies[2].BaseHP;
                Em.Def = GetEnemies[2].BaseDef;
                Em.SPD = GetEnemies[2].BaseSpeed;
                Em.EXP = GetEnemies[2].EXP;
                //Em.weakness = db.SelectEffect((GetEnemies[2].EffectID))[0];
                break;
        }
        switch (a/3) {
            case 0: 
                Em.Hp += (currentWave*5)-50;
                Em.SPD += 15;
                break;
            case 1:
                Em.Hp += currentWave * 5;
                break;
            case 2:
                Em.Hp += (currentWave * 5)+50;
                Em.SPD -= 15;
                break;
        }
        Em.GetComponent<NavMeshAgent>().speed = Mathf.FloorToInt(Em.SPD/10);
        Em.GetComponent<NavMeshAgent>().angularSpeed = Em.SPD;
        Em.EffectPrefabs = EffectPrefabs;
        Em.Score = score;
        return myPrefabs[a];
    }
    public void Enemies() {
        //GetLevel = db.LevelSelect(lvlId);
        waves = Getwaves();
        //GetWaves = db.SelectWave(waves[currentWave]);
        Debug.Log($"{waves[currentWave]} | { currentWave+1 }");
        //GetEffects = db.SelectEffects();
        GetEnemies = db.SelectOrigami();
        max = waves[currentWave];
    }
    private int[] Getwaves()
    {
        int waveMax = 4 + (2 * lvlId);
        int[] temp = new int[waveMax];
        for (int i = 0; i < waveMax; i++)
        {
            temp[i] = 3 * (i+1) + lvlId;
        }
        return temp;
    }
}