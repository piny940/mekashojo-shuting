using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__RepeatedFire_scr : Enemy__BulletBaseImp
{
    [SerializeField, Header("弾を発射する間隔(フレーム)")] float _shortFiringInterval;
    int _firingCount;
    int _frameCount;
    Vector3 _firingDirection;
    bool _hasFiringDirectionSet;

    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.RepeatedFire__MiddleDrone;

        BulletEnemyInitialize();

        _firingCount = 0;
        _frameCount = 0;
        _hasFiringDirectionSet = false;
    }


    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        commonForBattleScenes.Pause(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack(_shortFiringInterval, ref _firingCount, ref _frameCount, ref _firingDirection, ref _hasFiringDirectionSet);

        DestroyLater();
    }
}
