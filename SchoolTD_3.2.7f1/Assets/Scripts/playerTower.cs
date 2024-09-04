using System.Collections;
using System.Collections.Generic;
public class playerTower 
{
    private int id;
    private int towerID;
    private int playerID;
    private int exp;

    public playerTower(int towerID, int playerID, int exp)
    {
        this.towerID = towerID;
        this.playerID = playerID;
        this.exp = exp;
    }

    public playerTower(int id, int towerID, int playerID, int exp)
    {
        this.id = id;
        this.towerID = towerID;
        this.playerID = playerID;
        this.exp = exp;
    }

    public int Id { get => id; set => id = value; }
    public int TowerID { get => towerID; set => towerID = value; }
    public int PlayerID { get => playerID; set => playerID = value; }
    public int Exp { get => exp; set => exp = value; }
}
