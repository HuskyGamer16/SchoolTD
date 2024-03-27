using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Timers;
public class SpawnEnemy : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public float timeToSpawn;
    public float spawnCooldown;
    public GameObject StartPoint;
    int amount = 1;
    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
            spawnCooldown = timeToSpawn;
    }
    private void Update()
    {
        if(amount <= 10 ){
        spawnCooldown -= Time.deltaTime; 
        }
        if(spawnCooldown<=0){
            Instantiate(myPrefab, StartPoint.transform.position, Quaternion.Euler(new Vector3(-90, -180, 0)));
        spawnCooldown = timeToSpawn;
        amount++;
       }
    }
}