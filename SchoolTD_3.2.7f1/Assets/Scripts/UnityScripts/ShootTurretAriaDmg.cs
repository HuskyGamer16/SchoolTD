using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTurretAriaDmg : MonoBehaviour
{
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<GameObject> targets;
    public List<int> targetExps;
    public float dmgRate = 0.1f;
    private float dmgCooldown;
    public int towerid;
    public int towerdmg;
    void Start()
    {
        dmgCooldown = dmgRate;
    }
    public void Shoot()
    {
        try
        {
            if (targets.Count != 0)
            {
                dmgCooldown -= Time.deltaTime;
                if (dmgCooldown <= 0)
                {
                    foreach (var target in targets)
                    {
                        EnemyMovement EM = target.GetComponent<EnemyMovement>();
                        switch (EM.effect.Id)
                        {
                            case 3:
                                EM.effect = EnemyMovement.GetEffects[8];
                                break;
                            case 4:
                                EM.effect = EnemyMovement.GetEffects[6];
                                break;
                            case 5:
                                EM.effect = EnemyMovement.GetEffects[5];
                                break;
                            default:
                                EM.effect = EnemyMovement.GetEffects[1];
                                break;
                        }
                        EM.DMG = towerdmg;
                        EM.ElecPlus = true;
                    }
                    dmgCooldown = dmgRate;
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
        catch (NullReferenceException) {
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
            while (j < targets.Count && targets[j].gameObject != other.gameObject){ j++; }
            if (j >= targets.Count) { 
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
        transform.Rotate(0, -25 * Time.deltaTime, 0);
        Shoot();
    }
}