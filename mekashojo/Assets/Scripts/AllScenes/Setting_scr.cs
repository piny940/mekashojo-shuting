using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_scr : MonoBehaviour
{
    public static Setting_scr setting = null;

    public int bgmVolume { get; set; }
    public int seVolume { get; set; }
    public int mouseSensitivity { get; set; }
    public char forwardKey { get; set; }
    public char backKey { get; set; }
    public char leftKey { get; set; }
    public char rightKey { get; set; }

    //シングルトン
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
