using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPlayerHP : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Destroy(other.gameObject);
        }
    }
}
