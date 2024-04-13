using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTurretBasic : MonoBehaviour
{
    [SerializeField]private GameObject Bullet;
    public List<GameObject> targets;
    public float bulletspeed;
    private  float timeToSpawn;
    private float spawnCooldown;

    void Start()
    {
        timeToSpawn = 2f;
        spawnCooldown = timeToSpawn;
    }
    private void Update()
    {
        try
        {
            Shoot();
        }
        catch (MissingReferenceException) {
            targets.Remove(targets[0]);
            Shoot();
        }
    }
    public void Shoot() {
        if (targets.Count != 0)
        {
            transform.LookAt(targets[0].transform);
            spawnCooldown -= Time.deltaTime;
            Vector3 spawnPos = transform.position;
            spawnPos.y += 1.5f;
            if (spawnCooldown <= 0)
            {
                GameObject NewBullet = Instantiate(Bullet, spawnPos, transform.rotation);
                NewBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * bulletspeed);
                spawnCooldown = timeToSpawn;
            }
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