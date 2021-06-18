using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewImage_scr : MonoBehaviour
{
    public Color color
    {
        get { return GetComponent<Image>().color; }
        set { GetComponent<Image>().color = value; }
    }
}
