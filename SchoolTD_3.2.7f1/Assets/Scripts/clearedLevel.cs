using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearedLevel : MonoBehaviour
{
    private int id;
    private int playerID;
    private int levelID;
    private int dif;
    private int score;

    public clearedLevel(int playerID, int levelID, int dif, int score)
    {
        this.playerID = playerID;
        this.levelID = levelID;
        this.dif = dif;
        this.score = score;
    }

    public clearedLevel(int id, int playerID, int levelID, int dif, int score)
    {
        this.id = id;
        this.playerID = playerID;
        this.levelID = levelID;
        this.dif = dif;
        this.score = score;
    }

    public int Id { get => id; set => id = value; }
    public int PlayerID { get => playerID; set => playerID = value; }
    public int LevelID { get => levelID; set => levelID = value; }
    public int Dif { get => dif; set => dif = value; }
    public int Score { get => score; set => score = value; }
}
