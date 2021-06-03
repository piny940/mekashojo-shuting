using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData_scr : MonoBehaviour
{
    public static ProgressData_scr progressData;
    public int latestStageNumber;   //ラストステージはステージ5として管理

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
