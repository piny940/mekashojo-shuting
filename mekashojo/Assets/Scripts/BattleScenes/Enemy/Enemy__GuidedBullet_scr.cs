using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__GuidedBullet_scr : EnemyBaseImp
{
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        _time = Random.Range(0, NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.FiringInterval]);
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        commonForBattleScenes.Pause(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack();

        //画面の外に出たら消滅する
        DestroyLater();
    }

    void Attack()
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            _time = 0;

            Instantiate((GameObject)Resources.Load("BattleScenes/EnemyFire__GuidedBullet"), transform.position, Quaternion.identity);
        }
    }
}
