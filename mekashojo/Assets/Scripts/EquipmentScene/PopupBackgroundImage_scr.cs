using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBackgroundImage_scr : MonoBehaviour
{
    public bool isVisible
    {
        get { return GetComponent<Image>().enabled; }
        set { GetComponent<Image>().enabled = value; }
    }
}
