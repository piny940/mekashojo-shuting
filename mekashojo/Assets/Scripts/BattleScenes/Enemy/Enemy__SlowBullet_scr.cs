using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SlowBullet_scr : Enemy__BulletBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.SlowBullet__SmallDrone;

        BulletEnemyInitialize();
    }


    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack();

        DestroyLater();
    }
}