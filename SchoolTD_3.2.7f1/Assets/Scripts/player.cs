using System.Collections;
using System.Collections.Generic;

public class player 
{
    private int id;
    private string username;
    private string pw;
    private int score;
    private int level;

    public player(string username, string pw)
    {
        this.username = username;
        this.pw = pw;
    }

    public player(string username, string pw, int score,int level)
    {
        this.username = username;
        this.pw = pw;
        this.score = score;
        this.level = level;
    }

    public player(int id, string username, string pw, int score, int level)
    {
        this.id = id;
        this.username = username;
        this.pw = pw;
        this.score = score;
        this.level = level;
    }

    public int Id { get => id; set => id = value; }
    public string Username { get => username; set => username = value; }
    public string Pw { get => pw; set => pw = value; }
    public int Score { get => score; set => score = value;}  
    public int Level { get => level; set => level = value; }
}
