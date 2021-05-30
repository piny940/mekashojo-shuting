using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData_scr : MonoBehaviour
{
    public static ProgressData_scr progressData;

    //シングルトン
    private void Awake()
    {
        if (progressData == null)
        {
            progressData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
