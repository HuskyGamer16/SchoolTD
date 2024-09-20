using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTurretBasic : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    [SerializeField]private GameObject Bullet;
    public List<GameObject> targets;
    public float bulletspeed;
    private  float timeToSpawn;
    private float spawnCooldown;
    int towerLvl;

    void Start()
    {
        timeToSpawn = 2f;
        spawnCooldown = timeToSpawn;
    }
    private void Update()
    {
        Shoot();
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
                    //NewBullet.GetComponent<BulletBehavior>().DMG = db.GetBulletDmg(towerLvl)
                    NewBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * bulletspeed * 4);
                    spawnCooldown = timeToSpawn;
                }
            }
        }
        catch (MissingReferenceException)
        {
            targets.Remove(targets[0]);
            Shoot();
        }
    }
private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.activeSelf)
        {
            targets.Remove(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy")) {
            targets.Add(other.gameObject);
        }
    }
}