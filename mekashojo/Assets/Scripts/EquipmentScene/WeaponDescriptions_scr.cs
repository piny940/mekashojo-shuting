using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDescriptions_scr : MonoBehaviour
{
    public string text
    {
        get { return GetComponent<Text>().text; }
        set { GetComponent<Text>().text = value; }
    }

    public bool isVisible
    {
        get { return GetComponent<Text>().enabled; }
        set { GetComponent<Text>().enabled = value; }
    }
}
