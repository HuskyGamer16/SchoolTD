using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTurretAOEDmg : MonoBehaviour
{
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<GameObject> targets;
    public List<int> targetExps;
    public float dmgRate = 0.2f;
    private float dmgCooldown;
    public int towerdmg;
    public int towerid;

    void Start()
    {
        dmgCooldown = dmgRate;
    }
    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {   try
        {
            if (targets.Count != 0)
            {
                transform.LookAt(targets[0].transform);
                dmgCooldown -= Time.deltaTime;
                if (dmgCooldown <= 0)
                {
                    foreach (var target in targets)
                    {
                        EnemyMovement EM = target.GetComponent<EnemyMovement>();
                        switch (EM.effect.Id)
                        {
                            case 2:
                                EM.effect = EnemyMovement.GetEffects[5];
                                break;
                            case 4:
                                EM.effect = EnemyMovement.GetEffects[9];
                                break; ;
                            default:
                                EM.effect = EnemyMovement.GetEffects[4];
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
            if(targetExps.Count>=1)
            {
                db.TowerXPgain(LoginHandler.playerid, towerid, targetExps[0]);
                targetExps.Remove(targetExps[0]);
            }
            if (targetExps.Count >= 1)
            {
                targets.Remove(targets[0]);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
            targetExps.Add(other.gameObject.GetComponent<EnemyMovement>().EXP);
        }
    }
}
