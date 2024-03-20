using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Timers;
public class SpawnEnemy : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public float speed = 4;
    int amount = 1;
    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
            

        // Instantiate at position (0, 0, 0) and zero rotation.

    }
   
    private void Update()
        {

        while (amount <= 10 )
                {
            //This summons as of now a shitload of prefabs, by the frame)
            amount++;
        Instantiate(myPrefab, new Vector3(-885, 18, 275), Quaternion.Euler(new Vector3(-90, -90, 0)));
        }
        //if (true) { false; }
        
    }
}