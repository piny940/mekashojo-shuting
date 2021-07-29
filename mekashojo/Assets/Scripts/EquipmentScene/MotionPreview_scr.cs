using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionPreview_scr : MonoBehaviour
{
    public Color color
    {
        get { return GetComponent<Image>().color; }
        set { GetComponent<Image>().color = value; }
    }

    public bool isVisible
    {
        get { return GetComponent<Image>().enabled; }
        set { GetComponent<Image>().enabled = value; }
    }
}