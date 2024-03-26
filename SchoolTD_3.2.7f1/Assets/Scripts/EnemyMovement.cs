using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject EndPos;
    private NavMeshAgent agent;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.Find("KeepAlive");
    }
    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += speed * Time.deltaTime * Vector3.back;
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
    }
}
