using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    List<effects> GetEffects = new();
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
            Debug.Log(tillWaveSpawn);
            if (tillWaveSpawn > 0)
            {
                tillWaveSpawn -= Time.deltaTime;
                waveFinished = false;
            }
        }
    }
    public void Spawn(int max)
    {
        if (amount < max)
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
        int max = enemyWeight[0] + enemyWeight[1] + enemyWeight[2];
        int r = Random.Range(0, max+1);
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
        switch (a%3) {
            case 0: 
                myPrefabs[a].GetComponent<EnemyMovement>().Hp *= 2/4*(GetWaves[0].Id%10);
                myPrefabs[a].GetComponent<EnemyMovement>().Hp += currentWave*5;
                myPrefabs[a].GetComponent<EnemyMovement>().SPD *= 3/2;
                break;
            case 1:
                myPrefabs[a].GetComponent<EnemyMovement>().Hp *= 1;
                myPrefabs[a].GetComponent<EnemyMovement>().Hp += currentWave * 5;
                myPrefabs[a].GetComponent<EnemyMovement>().SPD *= 1;
                break;
            case 2:
                myPrefabs[a].GetComponent<EnemyMovement>().Hp *= 3/2;
                myPrefabs[a].GetComponent<EnemyMovement>().Hp += currentWave * 5;
                myPrefabs[a].GetComponent<EnemyMovement>().SPD *= 2/4;
                break;
        }
        return myPrefabs[a];
    }
    public void Enemies() {
        //GetLevel = db.SelectLevel(1);
        Debug.Log(waveIds[currentWave]);
        GetWaves = db.SelectWave(waveIds[currentWave]);
        GetEffects = db.SelectEffects();
        GetEnemies = db.SelectOrigami(GetWaves[0].OrigamiId);
        max = GetWaves[0].EnemyTotal;
    }
    
}