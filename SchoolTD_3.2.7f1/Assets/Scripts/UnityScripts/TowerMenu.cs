using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TowerMenu : MonoBehaviour
{
    public string Menu;
    public GameObject[] Markers;
    public GameObject Camera;
    public GameObject Right;
    public GameObject Left;
    public int num = 0;
    
    public int camSpeed;
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
    public void right() {
        int pre = num;
        num++;
        if (!Left.activeSelf)
        {
            Left.SetActive(true);
        }
        else if(num == Markers.Length-1)
        {
            Right.SetActive(false);
        }
        
        position(pre);
    }
    public void left()
    {
        int pre = num;
        num--;
        if (!Right.activeSelf)
        {
            Right.SetActive(true);
        }
        else if (num == 0)
        {
            Left.SetActive(false);
        }
        position(pre);
        
    }
    public void position(int current) {
        Camera.transform.position = Vector3.MoveTowards(Markers[num].transform.position,Markers[current].transform.position,camSpeed*Time.deltaTime);
    }
    
}
