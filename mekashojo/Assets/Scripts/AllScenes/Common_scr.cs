using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_scr : MonoBehaviour
{
    public static Common_scr common = null;

    //シングルトン
    private void Awake()
    {
        if (common == null)
        {
            common = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
