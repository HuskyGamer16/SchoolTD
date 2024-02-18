using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    //public TMP_Text t;

    public TextMeshProUGUI Texttmp;
    public string seged; 

void Start()
{
        seged = Texttmp.text;
    Texttmp.color = Color.white;
        Debug.Log(seged);
}

void OnMouseOver()
{
    Texttmp.text = "Color.red";
}

void OnMouseExit()
{
        Texttmp.text = seged;
      Texttmp.color = Color.white;
}

// Update is called once per frame
void Update()
    {
        
    }
}
