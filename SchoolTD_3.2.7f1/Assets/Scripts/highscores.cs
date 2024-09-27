using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscores : MonoBehaviour
{
   private int id;
    private string username;
   private int score;

   public highscores(int id, string username, int score){
    this.id = id;
    this.username = username;
    this.score = score;
   }
   public highscores(string username, int score){
    this.username = username;
    this.score = score;
   }
   public int Id { get => id; set => id = value; }
    public string Username { get => username; set => username = value; }
    public int Score { get => score; set => score = value; }
}
