using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingStageTitleBase : MonoBehaviour
{
    public bool IsVisible
    {
        get { return this.GetComponent<Image>().enabled; }
        set { this.GetComponent<Image>().enabled = value; }
    }

    public void Start()
    {
        this.IsVisible = false;
    }
}
