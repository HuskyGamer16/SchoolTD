using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;
public class EnemyMovement : MonoBehaviour
{
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public GameObject EndPos;
    private NavMeshAgent agent;
    public int MaxHp;
    public int Hp;
    public int Def;
    public float SPD;
    public int DMG;
    public int EXP;
    public effects effect;
    public float effectCoolDown;
    public float CoolDownTime;
    public effects weakness;
    public TextMeshPro Score;
    static public List<effects> GetEffects;
    public bool ElecPlus = false;
    public bool Melt = false;
    public List<GameObject> EffectPrefabs;
    void Start()
    {
        effectCoolDown = 5f;
        CoolDownTime = effectCoolDown;
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.Find("Projector_Screen_done");
        MaxHp = Hp;
        effect = GetEffects[0];
    }
    void Update()
    {
        CoolDownTime -= Time.deltaTime;
        agent.destination = EndPos.transform.position;
        if (Hp <= 0)
        {
            CheckPlayerHP chp = EndPos.GetComponent<CheckPlayerHP>();
            chp.gradeNum += 1;
            chp.gradeSum += 5;
            int currentScore = Convert.ToInt32(Score.text);
            currentScore += EXP;
            Score.text = currentScore.ToString();
            CheckPlayerHP.score = currentScore;
            Destroy(gameObject);
        }
        if (effect == weakness)
        {
            DMG = Mathf.FloorToInt(DMG * 1.25f);
        }
        switch (effect.Id)
        {
            case 2:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[2], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    if ((CoolDownTime * 5) % 5 == 0)
                    {
                        Hp -= 5;
                    }
                }
                break;
            case 3:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[3], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    float temp = SPD;
                    SPD = temp * 3 / 4;
                    if (CoolDownTime < 0)
                    {
                        SPD = temp;
                    }
                }
                break;
            case 4:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[1], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    float temp = SPD;
                    SPD = 0;
                    if (CoolDownTime <= 4.5f) SPD = temp;
                }
                break;
            case 5:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[4], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    float temp = SPD;
                    if (CoolDownTime > 2.5f)
                    {
                        SPD = 0;
                    }
                    else if (CoolDownTime > 0)
                    {
                        SPD = temp / 2;
                    }
                    else
                    {
                        SPD = temp;
                    }
                }
                break;
            case 6:
                if (transform.childCount < 2)
                {
                    Destroy(gameObject.transform.GetChild(1).gameObject);
                    GameObject.Instantiate(EffectPrefabs[3], transform);
                    effect = GetEffects[2];
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    Melt = true;
                }
                break;
            case 7:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[0], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    Collider[] affected = Physics.OverlapSphere(transform.position, 25);
                    foreach (var col in affected)
                    {
                        if (col.gameObject.CompareTag("Enemy"))
                        {
                            col.gameObject.GetComponent<EnemyMovement>().Hp -= 20;
                        }
                    }
                    effect = GetEffects[0];
                }
                break;
            case 8:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[0], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    Collider[] affected = Physics.OverlapSphere(transform.position, 25);
                    foreach (var col in affected)
                    {
                        if (col.gameObject.CompareTag("Enemy") && col.gameObject.GetComponent<EnemyMovement>().effect == GetEffects[2])
                        {
                            col.gameObject.GetComponent<EnemyMovement>().effect = GetEffects[7];
                            col.gameObject.GetComponent<EnemyMovement>().Hp -= 5;
                        }
                    }
                    effect = GetEffects[0];
                }
                break;
            case 9:
                effect = GetEffects[0];
                break;
            case 10:
                if (transform.childCount < 2)
                {
                    GameObject.Instantiate(EffectPrefabs[1], transform);
                    Destroy(gameObject.transform.GetChild(1).gameObject, 5f);
                    float temp = SPD;
                    if (CoolDownTime > 4.5f)
                        SPD = 0;
                    else
                        SPD = temp;
                    ElecPlus = true;
                }
                break;
        }
        if (DMG > 0)
        {
            if (ElecPlus)
            {
                DMG += 20;
            }
            if (Melt)
            {
                DMG += 5;
            }
            Hp -= DMG;
            DMG = 0;
        }
        if (CoolDownTime < 0)
        {
            effect = GetEffects[0];
            CoolDownTime = effectCoolDown;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("projectile"))
        {
            Destroy(other.gameObject);
            DMG = other.GetComponent<BulletBehavior>().DMG;
        }
    }
}