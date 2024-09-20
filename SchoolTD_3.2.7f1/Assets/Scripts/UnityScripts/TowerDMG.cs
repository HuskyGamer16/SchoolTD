using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDMG : MonoBehaviour
{
    private float timeToSpawn;
    private float spawnCooldown;
    private bool isFlameOut;
    public int level;
    void Start()
    {
        // Dot DMG / hp- over time
        isFlameOut = false;
        timeToSpawn = 5f;
        spawnCooldown = timeToSpawn;
    }
    void Update()
    {
        if (isFlameOut)
        {
            if (spawnCooldown <= 0)
                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                isFlameOut = false;
        }
        else
        {
            if (spawnCooldown <= 0)
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                isFlameOut = true;
        }
    }
    private void Setfire(GameObject enemy)
    {
        effects preEffect = enemy.GetComponent<EnemyMovement>().effect;
        if (preEffect != null) {
            switch (preEffect.Id)
            {
                case 3:
                    preEffect = SpawnTower.GiveEffect(9);
                    break;
                case 4:
                    preEffect = SpawnTower.GiveEffect(7);
                    break;
                case 5:
                    preEffect = SpawnTower.GiveEffect(6);
                    break;
                default:
                    preEffect = SpawnTower.GiveEffect(2);
                    break;
            }
        }
        while (isFlameOut) {
            if ((spawnCooldown * 2) % 2 == 0) {
                int dmg = 0;
                switch (gameObject.GetComponent<TowerDMG>().level) {
                    case 1: dmg = 15; break;
                    case 2: dmg = 20; break;
                    case 3: dmg = 25; break;
                    default: dmg = 30; break;
                }
                    enemy.GetComponent<EnemyMovement>().Hp -= dmg;
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
                if(isFlameOut) Setfire(other.gameObject);
                spawnCooldown = timeToSpawn;
            }
        }
    }
}
