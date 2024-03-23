using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private int id;
    private string username;
    private string pw;
    
    public player(string username, string pw)
    {
        this.username = username;
        this.pw = pw;
    }

    public player(int id, string username, string pw)
    {
        this.id = id;
        this.username = username;
        this.pw = pw;
    }

    public int Id { get => id; set => id = value; }
    public string Username { get => username; set => username = value; }
    public string Pw { get => pw; set => pw = value; }

    
}
