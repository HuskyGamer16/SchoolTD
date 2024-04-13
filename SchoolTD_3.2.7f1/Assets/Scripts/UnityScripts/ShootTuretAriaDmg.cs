using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTuretAriaDmg : MonoBehaviour
{
    public List<GameObject> targets;
    public float dmgRate = 0.1f;
    private float dmgCooldown;

    void Start()
    {
        dmgCooldown = dmgRate;
    }
    private void Update()
    {
        if (targets.Count != 0)
        {
            dmgCooldown -= Time.deltaTime;
            if (dmgCooldown <= 0)
            {
                foreach (var target in targets)
                {
                    target.GetComponent<EnemyMovement>().Hp -= 1;
                }
                dmgCooldown = dmgRate;
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
        if (other.gameObject.activeSelf && other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }
}
