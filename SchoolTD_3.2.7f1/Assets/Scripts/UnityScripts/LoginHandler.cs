using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using TMPro;

public class LoginHandler : MonoBehaviour
{ 
    private bool IsGood;
    public GameObject panelFrom;
    public GameObject panelTo;
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
    public void ResetInputs()
    {
        sLog.text = "Please, Log in to play";
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
                pwError.text = "There is no one in the database with this username!";
            }
                if(count > 1)
            {
                pwError.enabled = true;
                pwError.text = "There is more than one person in the database with this username!";
            }
        }
    }
   
    public void CheckPW()
    {
        if (nameInput != null)
        {
            List<player> players =  db.SelectAllPlayer();

            int i = 0;
            while(i < players.Count && players[i].Username != nameInput.text)
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
            pwError.text = "You haven't filled out the form correctly!";
        }
        else
        {
            sLog.text = "Sikeres bejelentkezés!";
            pwError.text = "";
            pwError.enabled = true;
        }
    }
}
