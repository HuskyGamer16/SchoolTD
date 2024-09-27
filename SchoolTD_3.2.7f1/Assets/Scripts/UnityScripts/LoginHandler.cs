using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.Profiling;
public class LoginHandler : MonoBehaviour
{
    public float Delay = 5f;
    private float TimeTo;
    public bool stop = true;
    public LoginHandler instance;
    public static int playerid;
    private bool IsGood;
    public AnimationClip CamMovementReverse;
    public GameObject boulder;
    public GameObject panelFrom;
    public GameObject MainPanel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TMP_Text pwError;
    [SerializeField] private TMP_Text sLog;
    [SerializeField] private Camera cam;
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    public void Login()
    {
        if (!IsGood)
        {
            pwError.enabled = true;
            if (pwInput.text != "" && nameInput.text != "")
            {
                pwError.text = "Student name, and/or password do not match!";
            }
            else
                pwError.text = "You haven't filled out the form correctly!";
        }
        else
        {
            stop = !stop;
            sLog.text = "Successful Entrance!";
            pwError.text = "";
            pwError.enabled = true;
            boulder.SetActive(true);
            gameObject.SetActive(false);
            cam.GetComponent<Animator>().enabled = true;
            MainPanel.SetActive(true);
        }
    }
    public void ResetInputs()
    {
        if (playerid != 0)
        {
            IsGood = true;
            Login();
        }
        else { 
        playerid = 0;
        sLog.text = "To enter, show Student ID";
        nameInput.text = "";
        pwInput.text = "";
        IsGood = false;
        pwError.enabled = false;
        }
    }
    void Start()
    {
        Profiler.maxUsedMemory = 256 * 1024 *1024;
        TimeTo = Delay;
        ResetInputs();
    }
    public void UsernameGood()
    {
        if (nameInput != null)
        {
            int count = db.CountUserPlayer(nameInput.text);
            if (count == 1)
            {
                pwError.enabled = false;
                pwError.text = "";
            }
            else if (count <= 0)
            {
                pwError.enabled = true;
                pwError.text = "There is no one in the school with this name!";
            }
            if (count > 1)
            {
                pwError.enabled = true;
                pwError.text = "There is more than one person in the school with this name!";
            }
        }
    }
    public void CheckPW()
    {
        if (nameInput != null)
        {
            List<player> players = db.SelectAllPlayer();
            int i = 0;
            while (i < players.Count && players[i].Username != nameInput.text)
            {
                i++;
            }
            if (i < players.Count)
            {
                string pass = players[i].Pw;
                byte[] data = Encoding.UTF8.GetBytes(pwInput.text);
                data = new SHA512Managed().ComputeHash(data);
                string hash = Encoding.UTF8.GetString(data);
                if (pass == hash)
                {
                    IsGood = true;
                    playerid = players[i].Id;
                }
            }
        }
    }
    public void BackToMain()
    {
        Animator ani = cam.GetComponent<Animator>();
        //ani.speed = -1;
        ani.enabled = true;
        ani.GetComponent<Animation>().AddClip(CamMovementReverse, "CamMovementReverse");
        ani.PlayInFixedTime("CamMovementReverse");
        panelFrom.SetActive(false);
        MainPanel.SetActive(true);
    }
    private void Update()
    {
        if (!stop)
        {
            TimeTo -= Time.deltaTime;
            if (TimeTo <= 0)
            {
                stop = true;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            BackToMain();
        }
    }
}