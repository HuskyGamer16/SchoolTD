using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSelectLvl : MonoBehaviour
{
    public string name;
    public void ToLevelSelect() {
        SceneManager.LoadScene(name);
    }
}
