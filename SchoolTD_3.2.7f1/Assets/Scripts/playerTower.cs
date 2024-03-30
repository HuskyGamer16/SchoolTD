using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTower : MonoBehaviour
{
    private int id;
    private int towerID;
    private int playerID;
    private int currentXP;

    public playerTower(int towerID, int playerID, int currentXP)
    {
        this.towerID = towerID;
        this.playerID = playerID;
        this.currentXP = currentXP;
    }

    public playerTower(int id, int towerID, int playerID, int currentXP)
    {
        this.id = id;
        this.towerID = towerID;
        this.playerID = playerID;
        this.currentXP = currentXP;
    }

    public int Id { get => id; set => id = value; }
    public int TowerID { get => towerID; set => towerID = value; }
    public int PlayerID { get => playerID; set => playerID = value; }
    public int CurrentXP { get => currentXP; set => currentXP = value; }
}
