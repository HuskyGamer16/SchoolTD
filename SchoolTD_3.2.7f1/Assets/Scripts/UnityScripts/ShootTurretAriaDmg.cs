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
    private void Update()
    {
        transform.Rotate(0,-25*Time.deltaTime,0);
        
            Shoot();
        
    }
    public void Shoot()
    {   try
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
            db.TowerXPgain(LoginHandler.playerid, towerid, targetExps[0]);
            targets.Remove(targets[0]);
            targetExps.Remove(targetExps[0]);
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
            targetExps.Add(other.gameObject.GetComponent<EnemyMovement>().EXP);
            targets.Add(other.gameObject);
        }
    }
}
