using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private int id;
    private string username;
    private string password;
    private int score;

    public player(string username, string password, int score)
    {
        this.username = username;
        this.password = password;
        this.score = score;
    }

    public player(int id, string username, string password, int score)
    {
        this.id = id;
        this.username = username;
        this.password = password;
        this.score = score;
    }

    public int Id { get => id; set => id = value; }
    public string Username { get => username; set => username = value; }
    public string Password { get => password; set => password = value; }
    public int Score { get => score; set => score = value; }

    
}
