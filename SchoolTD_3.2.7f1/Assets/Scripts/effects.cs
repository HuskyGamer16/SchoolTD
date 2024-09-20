
using System.Collections;
using System.Collections.Generic;

public class effects 
{
    private int id;
    private string effectName;
    

    public effects(string effectName)
    {
        this.effectName = effectName;
    }

    public effects(int id, string effectName)
    {
        this.id = id;
        this.effectName = effectName;
    }

    public int Id { get => id; set => id = value; }
    public string EffectName { get => effectName; set => effectName = value; }
   
}
