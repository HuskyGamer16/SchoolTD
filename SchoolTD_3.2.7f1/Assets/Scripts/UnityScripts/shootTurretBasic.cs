using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTurretBasic : MonoBehaviour
{
    [SerializeField]private GameObject Bullet;
    private  float timeToSpawn;
    private float spawnCooldown;
    private int amount;
    void Start()
    {
        amount = 0;
        timeToSpawn = 3f;
        spawnCooldown = timeToSpawn;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.activeSelf)
        { 
        
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy")) {
            spawnCooldown -= Time.deltaTime;
            if (amount == 0)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity);
                spawnCooldown = timeToSpawn;
                amount = 1;
            }
                if (spawnCooldown <= 0)
            {
                Instantiate(Bullet,transform.position,Quaternion.identity);
                spawnCooldown = timeToSpawn;
            }
        }
    }
}
