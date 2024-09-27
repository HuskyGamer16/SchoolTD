using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using TMPro;

public class RegistryHandler : MonoBehaviour
{
    private float Delay;
    private bool chPw;
    private bool chLength;
    private bool chName;
    private bool IsGood;
    public GameObject panelFrom;
    public GameObject panelTo;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TMP_InputField pwAgainInput;
    [SerializeField] private TMP_Text pwError;
    [SerializeField] private TMP_Text sReg;
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    void Start()
    {
        Delay = 0;
        IsGood = false;
        pwError.enabled = false;
    }
    public void CheckAll(){
        if(chPw && chName && chLength){
            IsGood = true;
        }
    }
    public void ResetInputs() {
        sReg.text = "You can enroll here";
        nameInput.text = "";
        pwInput.text = "";
        pwAgainInput.text = "";
        IsGood = false;
        chName = false;
        chLength = false;
        chPw = false;
        Delay = 0;
        pwError.enabled = false;
    }
    public void UsernameGood()
    {
        if (nameInput != null)
        {
            int count = db.CountUserPlayer(nameInput.text);
            if (count > 0)
            {
                pwError.enabled = true;
                pwError.text = "This (user)name is already in use!";
                chName = false;
                IsGood = false;
            }
            else
            {
                pwError.enabled = false;
                pwError.text = "";
                chName = true;
            }
            CheckAll();
        }
    }
    public void IsPwLongEnough()
    {
        if (pwInput != null)
        {
            if (pwInput.text.Length <= 3)
            {
                pwError.text = "The password is not long enough! (4-16 characters)";
                pwError.enabled = true;
                chLength = false;
                IsGood = false;
            }
            else
            {
                pwError.text = "";
                pwError.enabled = false;
                chLength = true;
            }
            CheckAll();
        }
    }
    public void CheckPW()
    {

        if (pwInput != null && pwAgainInput != null)
        {
            if (pwInput.text != pwAgainInput.text)
            {
                pwError.text = "The passwords are not matching!";
                pwError.enabled = true;
                chPw = false;
                IsGood = false;
            }
            else
            {
                pwError.enabled = false;
                chPw = true;
            }
            CheckAll();
        }
    }
    
    public void Register()
    {
        if (!IsGood)
        {
            pwError.enabled = true;
            pwError.text = "You haven't filled out the form correctly!";
        }
        else
        {
            byte[] data = Encoding.UTF8.GetBytes(pwInput.text);
            data = new SHA512Managed().ComputeHash(data);
            string hash = Encoding.UTF8.GetString(data);
            db.InsertPlayer(new player(nameInput.text, hash));
            sReg.text = "Succesful Enrollment!";
            Delay = 1050f;
        }
    }
    private void FixedUpdate()
    {
        if (IsGood && Delay != 0)
        {
            Delay -= Time.unscaledTime;
            if (Delay < 0)
            {
                ResetInputs();
                panelFrom.SetActive(false);
                panelTo.SetActive(true);
            }
        }
    }
}