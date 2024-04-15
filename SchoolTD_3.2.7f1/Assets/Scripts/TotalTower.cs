using System.Collections;
using System.Collections.Generic;
public class TotalTower 
{
    private int id;
    private int towerMaxLVL;
    private int lvlUP;
    private int currentLVL;
    private int effectID;

    public TotalTower(int towerMaxLVL, int lvlUP, int currentLVL, int effectID)
    {
        this.towerMaxLVL = towerMaxLVL;
        this.lvlUP = lvlUP;
        this.currentLVL = currentLVL;
        this.effectID = effectID;
    }

    public TotalTower(int id, int towerMaxLVL, int lvlUP, int currentLVL, int effectID)
    {
        this.id = id;
        this.towerMaxLVL = towerMaxLVL;
        this.lvlUP = lvlUP;
        this.currentLVL = currentLVL;
        this.effectID = effectID;
    }

    public int Id { get => id; set => id = value; }
    public int TowerMaxLVL { get => towerMaxLVL; set => towerMaxLVL = value; }
    public int LvlUP { get => lvlUP; set => lvlUP = value; }
    public int CurrentLVL { get => currentLVL; set => currentLVL = value; }
    public int EffectID { get => effectID; set => effectID = value; }
}