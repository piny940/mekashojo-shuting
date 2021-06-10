using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SelfDestruct_scr : EnemyBaseImp
{
    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        PauseWithAnimation();
        
        //移動速度の設定
        SetVelocity();
        StartAnimation();
        
    }
}
