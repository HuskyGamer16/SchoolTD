using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTurretAOEDmg : MonoBehaviour
{
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public List<GameObject> targets;
    public float dmgRate = 0.1f;
    private float dmgCooldown;
    public int dmg;
    public TotalTower tower;

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
                        EM.DMG = dmg;
                        EM.ElecPlus = true;
                    }
                    dmgCooldown = dmgRate;
                }
            }
        }
        catch (MissingReferenceException)
        {
            db.TowerXPgain(LoginHandler.playerid, tower.Id, targets[0].GetComponent<EnemyMovement>().EXP);
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
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }
}
