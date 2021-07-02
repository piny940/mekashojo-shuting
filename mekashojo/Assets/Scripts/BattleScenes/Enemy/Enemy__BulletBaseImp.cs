using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__BulletBaseImp : EnemyBaseImp
{
    protected NormalEnemyData_scr.normalEnemyType normalEnemyType;

    float _time;
    GameObject _player;

    /// <summary>
    /// Startメソッドで呼ぶ<br></br>
    /// これより前にnormalEnemyTypeを設定する必要がある
    /// </summary>
    protected void BulletEnemyInitialize()
    {
        Initialize();

        //_timeの初期化
        _time = Random.Range(0, NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.FiringInterval]);

        //_playerの取得
        _player = GameObject.FindGameObjectWithTag(Common_scr.Tags.Player__BattleScene.ToString());

        if (_player == null)
        {
            throw new System.Exception();
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        //ポーズの処理
        commonForBattleScenes.Pause(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack();
    }

    void Attack()
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            _time = 0;

            //弾の生成
            GameObject bullet = Instantiate((GameObject)Resources.Load(EnemyFirePath(normalEnemyType)), transform.position, Quaternion.identity);

            //コンポーネントの取得
            Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();

            //null安全性
            if (bulletRigidbody2D == null)
            {
                throw new System.Exception();
            }

            //弾の速度の設定
            bulletRigidbody2D.velocity = (_player.transform.position - transform.position) * NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.FastBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed] / Vector3.Magnitude(_player.transform.position - transform.position);

            //ミサイルの場合は弾の向きを調整する
            if (normalEnemyType == NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone)
            {
                commonForBattleScenes.RotateToLookAt(bullet, transform.position, _player.transform.position);
            }
        }
    }

    /// <summary>
    /// 弾のPrefabのパスを返す
    /// </summary>
    /// <param name="normalEnemyType"></param>
    /// <returns></returns>
    string EnemyFirePath(NormalEnemyData_scr.normalEnemyType normalEnemyType)
    {
        string enemyType;

        switch (normalEnemyType)
        {
            case NormalEnemyData_scr.normalEnemyType.SingleBullet__SmallDrone:
                enemyType = "SingleBullet";
                break;

            case NormalEnemyData_scr.normalEnemyType.FastBullet__SmallDrone:
                enemyType = "FastBullet";
                break;

            case NormalEnemyData_scr.normalEnemyType.SlowBullet__SmallDrone:
                enemyType = "SlowBullet";
                break;

            case NormalEnemyData_scr.normalEnemyType.RepeatedFire__MiddleDrone:
                enemyType = "RepeatedFire";
                break;

            case NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone:
                enemyType = "StunBullet";
                break;

            case NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone:
                enemyType = "Missile";
                break;

            default:
                throw new System.Exception();
        }

        return "BattleScenes/EnemyFire__" + enemyType;
    }
}
