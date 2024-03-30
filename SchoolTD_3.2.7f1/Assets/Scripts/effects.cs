using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{
    private int id;
    private string effectName;
    private string affect;

    public effects(string effectName, string affect)
    {
        this.effectName = effectName;
        this.affect = affect;
    }

    public effects(int id, string effectName, string affect)
    {
        this.id = id;
        this.effectName = effectName;
        this.affect = affect;
    }

    public int Id { get => id; set => id = value; }
    public string EffectName { get => effectName; set => effectName = value; }
    public string Affect { get => affect; set => affect = value; }
}
