using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level__Title_scr : MonoBehaviour
{
    public bool isVisible
    {
        get { return GetComponent<Text>().enabled; }
        set { GetComponent<Text>().enabled = value; }
    }
}
