using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementButton_scr : MonoBehaviour
{
    public bool isActive
    {
        get { return GetComponent<Button>().interactable; }
        set { GetComponent<Button>().interactable = value; }
    }

    public bool isVisible
    {
        get { return this.gameObject.activeSelf; }
        set { this.gameObject.SetActive(value); }
    }

    public Action EnhanceAction;

    public void OnPush()
    {
        EnhanceAction();
    }
}
