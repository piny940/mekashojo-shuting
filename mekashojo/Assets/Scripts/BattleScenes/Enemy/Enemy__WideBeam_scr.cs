using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__WideBeam_scr : EnemyBaseImp
{
    // Start is called before the first frame update
    protected void Start()
    {
        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        Pause();

        //移動速度の設定
        SetVelocity();
    }
}
