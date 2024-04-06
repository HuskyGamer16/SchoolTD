using System.Collections;
using System.Collections.Generic;

public class waves 
{
    private int id;
    private int origamiId;
    private int enemyTotal;
    private int waveNum;

    public waves(int origamiId, int enemyTotal, int waveNum)
    {
        this.origamiId = origamiId;
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public waves(int id, int origamiId, int enemyTotal, int waveNum)
    {
        this.id = id;
        this.origamiId = origamiId;
        this.enemyTotal = enemyTotal;
        this.waveNum = waveNum;
    }

    public int Id { get => id; set => id = value; }
    public int OrigamiId { get => origamiId; set => origamiId = value; }
    public int EnemyTotal { get => enemyTotal; set => enemyTotal = value; }
    public int WaveNum { get => waveNum; set => waveNum = value; }
}