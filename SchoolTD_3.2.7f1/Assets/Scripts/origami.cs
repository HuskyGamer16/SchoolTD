using System.Collections;
using System.Collections.Generic;

public class origami
{
    private int id;
    private int baseHP;
    private int baseSpeed;
    private int baseDef;
    private int Exp;
    private int eff;

    public origami(int baseHP,int baseSpeed,int baseDef,int Exp,int eff) { 
        this.baseHP = baseHP;
        this.baseSpeed = baseSpeed;
        this.baseDef = baseDef;
        this.Exp = Exp;
        this.eff = eff;
    }
    public origami(int id,int baseHP, int baseSpeed, int baseDef, int Exp, int eff)
    {
        this.id = id;
        this.baseHP = baseHP;
        this.baseSpeed = baseSpeed;
        this.baseDef = baseDef;
        this.Exp = Exp;
        this.eff = eff;
    }
    public int ID { get => id; set => id = value; }
    public int BaseHP { get => baseHP; set => baseHP = value; }
    public int BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public int BaseDef { get => baseDef; set => baseDef = value; }
    public int EXP { get => Exp; set => Exp = value; }
    public int Eff { get => eff; set => eff = value; }
  
}
