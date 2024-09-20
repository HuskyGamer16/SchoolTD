using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 3f;
    public int DMG = 500;

    void Start()
    {
        if (DMG == 0){
        DMG = 500;
        }
        Debug.Log("shot: " + DMG);
        //Temporarily dmg = 500, for easier testing
        Destroy(gameObject, lifetime);
    }
}