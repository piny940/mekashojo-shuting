using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDescriptions_scr : MonoBehaviour
{
    public string text
    {
        get { return GetComponent<Text>().text; }
        set { GetComponent<Text>().text = value; }
    }
}
