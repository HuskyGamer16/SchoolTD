using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using System.Timers;
public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] myPrefabs = new GameObject[9];
    public float timeToSpawn;
    public float spawnCooldown;
    public GameObject StartPoint;
    bool waveFinished = false; 
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<effects> GetEffects = new();
    List<origami> GetEnemies = new();
    List<waves> GetWaves = new();
    List<levels> GetLevel = new();
    private float tillWaveSpawn = 0;
    int max;
    int amount = 0;
    int currentWave = 0;
    public int[] enemyWeight = new int[] {80,20,20};
    public int[] waveIds;

    void Start()
    {
        spawnCooldown = timeToSpawn;
        Enemies();
    }
    private void Update()
    {

        if (currentWave < waveIds.Length)
        {
            if (!waveFinished && tillWaveSpawn <=0)
            {

                if (!waveFinished) {
            
                    Spawn(max);
                }
                if (amount>=max) { 
                    //rajatok képernyõt
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
                Debug.Log(GetEnemies[0].BaseSpeed);
                break;
            case 1:
                Em.Hp = GetEnemies[1].BaseHP;
                Em.Def = GetEnemies[1].BaseDef;
                Em.SPD = GetEnemies[1].BaseSpeed;
                break;
            case 2:
                Em.Hp = GetEnemies[2].BaseHP;
                Em.Def = GetEnemies[2].BaseDef;
                Em.SPD = GetEnemies[2].BaseSpeed;
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

        return myPrefabs[a];
    }
    public void Enemies() {
        //GetLevel = db.SelectLevel(1);
        GetWaves = db.SelectWave(waveIds[currentWave]);
        Debug.Log($"{waveIds[currentWave]} | { currentWave }");
        GetEffects = db.SelectEffects();
        GetEnemies = db.SelectOrigami();
        max = GetWaves[0].EnemyTotal;
    }
    
}