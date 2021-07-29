using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_scr : MonoBehaviour
{
    public static Setting_scr setting = null;

    public float bgmVolume { get; set; }
    public float seVolume { get; set; }
    public float mouseSensitivity { get; set; }
    public char forwardKey { get; set; }
    public char backKey { get; set; }
    public char leftKey { get; set; }
    public char rightKey { get; set; }

    private void Awake()
    {
        if (setting == null)
        {
            setting = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
