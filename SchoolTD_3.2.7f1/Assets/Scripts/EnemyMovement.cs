using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speed = 1f;
    Vector3 direction;
    ColliderHit hit;
    // Start is called before the first frame update
    void Start()
    {
        WaitForSecondsRealtime a = new WaitForSecondsRealtime(2.5f);
        a.waitTime = 2;
        a.MoveNext();
        Movement();
        a.Reset();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void Movement() {
        
        Vector3 endpoint = new Vector3(-880,20,265);
        while (!hit.collider && transform.position.z >= endpoint.z) {
            direction = Vector3.back;
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
