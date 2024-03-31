using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 25f;
    public GameObject EndPos;
    private NavMeshAgent agent;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.Find("KeepAlive");
    }
    void Update()
    {
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
    }
}
