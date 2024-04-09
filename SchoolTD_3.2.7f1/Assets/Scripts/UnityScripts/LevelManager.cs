using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string Menu;
    public string Level1;
    public string Level2;
    public string Level3;
    public string Level4;
    public string Level5;
    public string Level6;
    public GameObject[] Doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        SceneManager.LoadScene(Menu);
    }
    public void lvl1()
    {
        SceneManager.LoadScene(Level1);
    }
    public void lvl2()
    {
        SceneManager.LoadScene(Level2);
    }
    public void lvl3()
    {
        SceneManager.LoadScene(Level3);
    }
    public void lvl4()
    {
        SceneManager.LoadScene(Level4);
    }
    public void lvl5()
    {
        SceneManager.LoadScene(Level5);
    }
    public void lvl6()
    {
        SceneManager.LoadScene(Level6);
    }
}
