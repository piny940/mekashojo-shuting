using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__WideBeam_scr : EnemyBaseImp
{
    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        PauseWithoutAnimation();

        //移動速度の設定
        SetVelocity();
    }
}
