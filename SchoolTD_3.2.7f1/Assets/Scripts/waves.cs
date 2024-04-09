using System.Collections;
using System.Collections.Generic;

public class waves 
{
    private int id;
    private int enemyTotal;
    private int waveNum;

    public waves( int enemyTotal, int waveNum)
    {
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public waves(int id, int enemyTotal, int waveNum)
    {
        this.id = id;
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public int Id { get => id; set => id = value; }
    public int EnemyTotal { get => enemyTotal; set => enemyTotal = value; }
    public int WaveNum { get => waveNum; set => waveNum = value; }
}