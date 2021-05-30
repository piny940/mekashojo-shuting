using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyData_scr : MonoBehaviour
{
    public static NormalEnemyData_scr normalEnemyData = null;

    //シングルトン
    private void Awake()
    {
        if (normalEnemyData == null)
        {
            normalEnemyData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
