using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 25f;
    public GameObject EndPos;
    private NavMeshAgent agent;
    private int Hp;

    void Start(){
        Hp = 1;
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.Find("KeepAlive");
    }
    void Update()
    {
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
        if (Hp <= 0) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("projectile")) {
            Destroy(other.gameObject);
            Hp--;
        }
    }
}
