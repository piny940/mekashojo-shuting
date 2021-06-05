using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_scr : MonoBehaviour
{
    public static Common_scr common = null;

    public enum Tags
    {
        Enemy_BattleScene,
        Enemy__Fire_BattleScene,
        Player__Fire_BattleScene,
        PauseController_BattleScene,
        StartCount_BattleScene
    }

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
