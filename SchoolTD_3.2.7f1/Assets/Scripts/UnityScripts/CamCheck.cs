using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf && other.CompareTag("Player"))
        {
            Debug.Log("Entered!");
        }
    }
}
