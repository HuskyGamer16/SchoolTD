using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bulletGo : MonoBehaviour
{
    public float speed = 25f;
    private GameObject EndPos;
    private NavMeshAgent agent;
    public float lifetime = .75f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        if (EndPos == null) {
            EndPos = GameObject.FindGameObjectWithTag("Enemy");
            if (EndPos == null)
            {
                Destroy(gameObject);
            }
        }
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
        agent.angularSpeed = 270f;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
         
        lifetime -= Time.deltaTime;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform.position == EndPos.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
