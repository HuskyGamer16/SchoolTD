using System.Collections;
using System.Collections.Generic;
public class TotalTower 
{
    private int id;
    private string name;
    private int dmg;
    private int lvl;
    private int exp;

    public TotalTower(string name, int dmg, int lvl, int exp)
    {
        this.name = name;
        this.dmg = dmg;
        this.lvl = lvl;
        this.exp = exp;
    }

    public TotalTower(int id, string name, int dmg, int lvl, int exp)
    {
        this.id = id;
        this.name = name;
        this.dmg = dmg;
        this.lvl = lvl;
        this.exp = exp;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Lvl { get => lvl; set => lvl = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    public int Exp { get => exp; set => exp = value; }
}