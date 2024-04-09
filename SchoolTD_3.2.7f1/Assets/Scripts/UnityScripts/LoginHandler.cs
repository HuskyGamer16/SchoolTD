using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using TMPro;

public class LoginHandler : MonoBehaviour
{
    public LoginHandler instance;
    public int playerid;
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
    //Yes, it is a vulnerability to not have functional database enccryption and passwords, but we had to save time, it is what it is. If we gonna make this a proper game we'd do it but this is a demo at best 
    void Start()
    {
        ResetInputs();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            BackToMain();
        }
    }
    public void ResetInputs()
    {
        playerid = 0;
        sLog.text = "To enter, show Student ID";
        nameInput.text = "";
        pwInput.text = "";
        IsGood = false;
        pwError.enabled = false;
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
                Debug.Log($"this part works, you can do it, there is {count} amount of same usernames");
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
            if (i < players.Count) {
                string pass = players[i].Pw;
                byte[] data = Encoding.UTF8.GetBytes(pwInput.text);
                data = new SHA512Managed().ComputeHash(data);
                string hash = Encoding.UTF8.GetString(data);
                if (pass == hash) {
                    Debug.Log("Azonos!");
                    IsGood = true;
                    playerid = players[i].Id;
                }
                else
                {
                    Debug.Log("u stupid");
                }
            }
        }
    }
    public void Login()
    {
        if (!IsGood)
        {
            pwError.enabled = true;
            if (pwInput.text != null && nameInput.text != null)
            {
                pwError.text = "Student name, and/or password do not match!";
            }
            else
                pwError.text = "You haven't filled out the form correctly!";
        }
        else
        {
            sLog.text = "Successful Entrance!";
            pwError.text = "";
            pwError.enabled = true;
            boulder.SetActive(true);
            cam.GetComponent<Animator>().enabled = true;
            SendToSelect();
        }
    }
    public void SendToSelect(){
        Animator ani = cam.GetComponent<Animator>();
        ani.speed = 1;
        if (!ani.GetComponent<Animation>().isPlaying)
            {
            ani.enabled = false;
            panelFrom.SetActive(false);
        }
    }
    public void BackToMain() { 
        Animator ani = cam.GetComponent<Animator>();
        //somehow i have to make it transition backwards, or make a new anim with the reverse of the orig 
        //ani.speed = -1;
        ani.enabled = true;
        ani.GetComponent<Animation>().AddClip(CamMovementReverse, "CamMovementReverse");
        ani.PlayInFixedTime("CamMovementReverse");
        panelFrom.SetActive(false);
        MainPanel.SetActive(true);
    }
}
