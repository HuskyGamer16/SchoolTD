using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bulletGo : MonoBehaviour
{
    public float speed = 25f;
    public GameObject EndPos;
    private NavMeshAgent agent;
    public float lifetime = 3f;
    public int DMG;
    void Start()
    {
        DMG = 500;
        agent = GetComponent<NavMeshAgent>();
        EndPos = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        if (EndPos == null)
        {
            EndPos = GameObject.FindGameObjectWithTag("Enemy");
            if (EndPos == null)
            {
                Destroy(gameObject);
            }
        }
        agent.speed = speed;
        agent.destination = EndPos.transform.position;
        agent.angularSpeed = 270f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform.position == EndPos.transform.position)
        {
            Destroy(gameObject,lifetime);
        }
    }

}
