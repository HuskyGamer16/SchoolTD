using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hiscore : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> TMPList;
    static DbConnect db = new DbConnect("127.0.0.1", "schooltd", "root", "");
    List<highscores> scores;
    void Start()
    {
        scores = db.GetHighScores();
        for (int i = 0; i < TMPList.Count; i++)
        {
            try
            {
                TMPList[i].text = scores[i].Username + " " + Convert.ToString(scores[i].Score);
            }
            catch (ArgumentOutOfRangeException)
            {
                TMPList[i].text = "";
            }
        }
    }
}
