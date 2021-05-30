using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_scr : MonoBehaviour
{
    public static Setting_scr setting = null;

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
