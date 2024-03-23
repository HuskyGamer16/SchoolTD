using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscores : MonoBehaviour
{
   private int id;
   private int playerId;
   private int score;

   public highscores(int id, int playerId, int score){
    this.id = id;
    this.playerId = playerId;
    this.score = score;
   }
   public highscores(int playerId, int score){
    this.playerId = playerId;
    this.score = score;
   }
   public int Id { get => id; set => id = value; }
    public int PlayerId{ get => playerId; set => playerId = value; }
    public int Score { get => score; set => score = value; }
}
