using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bulletGo : MonoBehaviour
{
    public float speed = 25f;
    private GameObject EndPos;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
        agent.angularSpeed = 180f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform.position == EndPos.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
