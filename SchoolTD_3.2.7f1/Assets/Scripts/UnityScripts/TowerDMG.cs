using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDMG : MonoBehaviour
{
    public GameObject Tower;
    public TotalTower TowerDetails;
    private float timeToSpawn;
    private float spawnCooldown;
    private bool isFlameOut;
    
    void Start()
    {
        isFlameOut = false;
        timeToSpawn = 5f;
        spawnCooldown = timeToSpawn;
    }
    void Update()
    {
        if(spawnCooldown < 0)
        if (isFlameOut)
        {

        }
        else { 
        
         }
    }
    private void Setfire(GameObject enemy)
    {
       effects preEffect = enemy.GetComponent<effects>();
        if (preEffect != null) {
            switch (preEffect.Id) {
                case 3:
                    enemy.GetComponent<EnemyMovement>().effect =  9;
                    break;
                case 4:
                    enemy.GetComponent<EnemyMovement>().Id = 7;
                    break;
                case 5:
                    enemy.GetComponent<EnemyMovement>().Id = 6;
                    break;
                default:
                    enemy.GetComponent<EnemyMovement>().Id = 2;
                    break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown <= 0)
            {
                Setfire(other.gameObject);
                spawnCooldown = timeToSpawn;
            }
        }
    }
}
