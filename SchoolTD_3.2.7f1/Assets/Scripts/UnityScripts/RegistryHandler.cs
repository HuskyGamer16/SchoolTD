using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using TMPro;

public class RegistryHandler : MonoBehaviour
{
    private float Delay;
    private bool IsGood;
    public GameObject panelFrom;
    public GameObject panelTo;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField pwInput;
    [SerializeField] private TMP_InputField pwAgainInput;
    [SerializeField] private TMP_Text pwError;
    [SerializeField] private TMP_Text sReg;
    DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    //Yes, it is a vulnerability to not have functional database enccryption and passwords, but we had to save time, it is what it is. If we gonna make this a proper game we'd do it but this is a demo at best 
    void Start()
    {
        Delay = 0;
        IsGood = false;
        pwError.enabled = false;
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
    public void ResetInputs() {
        sReg.text = "You can sign-up here";
        nameInput.text = "";
        pwInput.text = "";
        pwAgainInput.text = "";
        IsGood = false;
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
                pwError.text = "This username is already in use!";
            }
            else
            {
                pwError.enabled = false;
                pwError.text = "";
                Debug.Log($"this part works, you can do it, there is {count} amount of same usernames");
            }
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
            }
            else
            {
                pwError.text = "";
                pwError.enabled = false;
            }
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
            }
            else
            {
                pwError.enabled = false;
                IsGood = true;
                Debug.Log("isverygud (tm)");
            }
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
            Debug.Log(hash);
            db.InsertPlayer(new player(nameInput.text, hash));
            //This is not safe by any means, but time is running low, so we have to deal with it, desperate times calls for desperate measures.
            sReg.text = "Succesful registration!";
            Delay = 1050f;
        }
    }
}