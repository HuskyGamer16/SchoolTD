using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ResolutionChange : MonoBehaviour
{
    private TextMeshProUGUI text;
    private TextMeshProUGUI res;
    private Resolution[] Resol;
    private List<Resolution> filteredRes;
    private float currentRefRate;
    private int currentResIndex = 0;
    private string[] res_available = new string[]{"3840x2160","2560x1440","2048x1152","1920x1080","1366x768","1280x720","960x540","640x360"};

    private void Start()
    {
        Resol = Screen.resolutions;
        filteredRes = new List<Resolution>();
    }
    public void IncRes()
    {
    
    }
    public void DecRes() 
    {
    
    }
}
