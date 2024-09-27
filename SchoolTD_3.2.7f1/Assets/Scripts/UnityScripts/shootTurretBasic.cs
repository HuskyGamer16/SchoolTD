using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTurretBasic : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    [SerializeField]private GameObject Bullet;
    public List<GameObject> targets;
    public List<int> targetExps;
    public float bulletspeed;
    private  float timeToSpawn;
    private float spawnCooldown;
    public int towerid;
    public int towerdmg;
    void Start()
    {
        timeToSpawn = 2f;
        spawnCooldown = timeToSpawn;
    }
    public void Shoot()
    {
        try
        {
            if (targets.Count != 0)
            {
                transform.LookAt(targets[0].transform);
                spawnCooldown -= Time.deltaTime;
                Vector3 spawnPos = transform.position;
                spawnPos.y += 1.5f;
                if (spawnCooldown <= 0)
                {
                    Animator Anim = gameObject.GetComponent<Animator>();
                    Anim.SetTrigger("shoot");
                    GameObject NewBullet = Instantiate(Bullet, spawnPos, transform.rotation);
                    Debug.Log(towerdmg);
                    NewBullet.GetComponent<BulletBehavior>().DMG = towerdmg;
                    NewBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * bulletspeed * 4);
                    spawnCooldown = timeToSpawn;
                }
            }
        }
        catch (MissingReferenceException)
        {
            if (targetExps.Count >= 1)
            {
                db.TowerXPgain(LoginHandler.playerid, towerid, targetExps[0]);
                targetExps.Clear();
            }
            if (targets.Count >= 1)
            {
                targets.Clear();
            }
            Shoot();
        }
        catch (NullReferenceException)
        {
            if (targetExps.Count >= 1)
            {
                db.TowerXPgain(LoginHandler.playerid, towerid, targetExps[0]);
                targetExps.Clear();
            }
            if (targets.Count >= 1)
            {
                targets.Clear();
            }
            Shoot();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.activeSelf)
        {
            targets.Remove(other.gameObject);
            targetExps.Remove(other.gameObject.GetComponent<EnemyMovement>().EXP);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            int j = 0;
            while (j < targets.Count && targets[j].gameObject != other.gameObject) { j++; }
            if (j >= targets.Count)
            {
                targetExps.Add(other.gameObject.GetComponent<EnemyMovement>().EXP);
                targets.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            targetExps.Add(other.gameObject.GetComponent<EnemyMovement>().EXP);
            targets.Add(other.gameObject);
        }
    }
    private void Update()
    {
        Shoot();
    }
}