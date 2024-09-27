using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public int Manager;
   public void ChangeCam() {
        //CamChange not working, plays once at start, not ever again
        GetComponent<Animator>().SetTrigger("Change");
    }
    void Cam1En() {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    void Cam2En()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }
    public void ManageCams() {
        if (Manager == 0)
        {
            Cam2En();
            Manager = 1;
        }
        else { 
            Cam1En();
            Manager = 0;
        }
    }
}