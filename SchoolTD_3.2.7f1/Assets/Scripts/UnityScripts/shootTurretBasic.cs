using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTurretBasic : MonoBehaviour
{
    [SerializeField]private GameObject Bullet;
    private  float timeToSpawn;
    private float spawnCooldown;
    void Start()
    {
        timeToSpawn = 5f;
        spawnCooldown = timeToSpawn;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy")) {
            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown <= 0)
            {
                Instantiate(Bullet,transform.position,Quaternion.identity);
                spawnCooldown = timeToSpawn;
            }
        }
    }
}
