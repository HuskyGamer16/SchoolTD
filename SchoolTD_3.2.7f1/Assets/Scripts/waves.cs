using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves : MonoBehaviour
{
    private int id;
    private int enemyType;
    private int enemyTotal;
    private int waveNum;

    public waves(int enemyType, int enemyTotal, int waveNum)
    {
        this.enemyType = enemyType;
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public waves(int id, int enemyType, int enemyTotal, int waveNum)
    {
        this.id = id;
        this.enemyType = enemyType;
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public int Id { get => id; set => id = value; }
    public int EnemyType { get => enemyType; set => enemyType = value; }
    public int EnemyTotal { get => enemyTotal; set => enemyTotal = value; }
    public int WaveNum { get => waveNum; set => waveNum = value; }
}