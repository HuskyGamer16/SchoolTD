using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //public float speed = 25f;
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public GameObject EndPos;
    private NavMeshAgent agent;
    public int Hp;
    public int Def;
    public float SPD;
    public int DMG;
    public effects effect;
    bool IsShot = false;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.Find("Projector_Screen_done");
    }
    void Update()
    {
        //agent.speed = speed;
        agent.destination = EndPos.transform.position;
        if (Hp <= 0) {
            Destroy(gameObject);
        }/*switch (effect.Id) { 
            case 2:
                break; 
            case 3:
                break; 
            case 4:
                break; 
            case 5:
                break; 
            case 6:
                break; 
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            default:
                effect.Id = effect.Id;
                break;
        }*/
        if (IsShot) {
            Hp -= DMG;
            DMG = 0;
            IsShot = false;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("projectile")) {
            Destroy(other.gameObject);
            DMG = other.GetComponent<BulletBehavior>().DMG;
            IsShot = true;
        }
    }
}