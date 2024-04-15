using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ResolutionChange : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resDropDown;
    private TextMeshProUGUI text;
    private TextMeshProUGUI res;
    private Resolution[] Resol;
    private List<Resolution> filteredRes;

    private RefreshRate currentRefRate;
    private int currentResIndex = 0;
    public GameObject Settings;
    
    private void Start()
    {
        //Resol = Screen.resolutions;
        //filteredRes = new List<Resolution>();
        ////resDropDown.ClearOptions();
        //currentRefRate = Screen.currentResolution.refreshRateRatio;
        //for (int i = 0; i < Resol.Length; i++)
        //{
        //    if (Resol[i].refreshRateRatio.value == currentRefRate.value) {
        //        filteredRes.Add(Resol[i]);
        //    }
        //}

        //List<string> options = new List<string>();
        //for (int i = 0; i < filteredRes.Count; i++)
        //{
        //    string filteredOpt = filteredRes[i].width +"x"+  filteredRes[i].height + " "+filteredRes[i].refreshRateRatio.ToString() + " Hz";
        //    options.Add(filteredOpt);
        //    if (filteredRes[i].width == Screen.width && filteredRes[i].height == Screen.height) {
        //        currentResIndex = i;
        //    }
        //}

        //resDropDown.AddOptions(options);
        //resDropDown.value = currentResIndex;
        //resDropDown.RefreshShownValue();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)){
            Debug.Log("Itt escape van");
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1f;
                Settings.SetActive(false);

            }
            else
            {
                Time.timeScale = 0f;
                Settings.SetActive(true);
            }
        }
    }
    public void SetRes(int resIndex) { 
        //Resolution resolution = filteredRes[resIndex];
        //Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
