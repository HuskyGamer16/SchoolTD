using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 3f;
    public int DMG;

    void Start()
    {
        DMG = 50;
        Destroy(gameObject, lifetime);
    }
}