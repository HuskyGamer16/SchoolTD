using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class getscore : MonoBehaviour
{
    public TextMeshPro board;
    void Start()
    {
        gameObject.GetComponent<TextMeshPro>().text = board.text;
    }
    private void Update()
    {
        gameObject.GetComponent<TextMeshPro>().text = board.text;
    }
}
