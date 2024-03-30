using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levels : MonoBehaviour
{
    private int id;
    private int waveID;
    private int waveTotal;
    private int maxHP;
    private int maxBuildables;

    public levels(int waveID, int waveTotal, int maxHP, int maxBuildables)
    {
        this.waveID = waveID;
        this.waveTotal = waveTotal;
        this.maxHP = maxHP;
        this.maxBuildables = maxBuildables;
    }

    public levels(int id, int waveID, int waveTotal, int maxHP, int maxBuildables)
    {
        this.id = id;
        this.waveID = waveID;
        this.waveTotal = waveTotal;
        this.maxHP = maxHP;
        this.maxBuildables = maxBuildables;
    }

    public int Id { get => id; set => id = value; }
    public int WaveID { get => waveID; set => waveID = value; }
    public int WaveTotal { get => waveTotal; set => waveTotal = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int MaxBuildables { get => maxBuildables; set => maxBuildables = value; }
}
