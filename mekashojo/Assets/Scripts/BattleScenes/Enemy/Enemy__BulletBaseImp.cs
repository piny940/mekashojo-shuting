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
        _player = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.Player__BattleScene.ToString());

        if (_player == null)
        {
            throw new System.Exception();
        }
    }

    /// <summary>
    /// 攻撃処理のメソッド
    /// </summary>
    protected void Attack()
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            _time = 0;

            Vector3 modifiedThisPosition = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);

            Fire((_player.transform.position - modifiedThisPosition) * NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed] / Vector3.Magnitude(_player.transform.position - modifiedThisPosition));
        }
    }


    /// <summary>
    /// 発射速度のリストを引数に受け取った時はSpreadBulletかWideSpreadBulletと判断
    /// </summary>
    /// <param name="fireDirections"></param>
    protected void Attack(List<Vector3> fireDirections)
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            _time = 0;

            for (int i = 0; i < fireDirections.Count; i++)
            {
                Fire(fireDirections[i]);
            }
        }
    }


    /// <summary>
    /// floatとref intを引数に受け取った時はRepeatedFireと判断
    /// </summary>
    /// <param name="shortFiringInterval"></param>
    protected void Attack(float shortFiringInterval, ref int firingCount, ref int frameCount, ref Vector3 firingDirection, ref bool isFiringDirectionSet)
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            frameCount++;

            //発射速度の設定
            if (!isFiringDirectionSet)
            {
                Vector3 position__Modified = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);

                firingDirection = (_player.transform.position - position__Modified) * NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed] / Vector3.Magnitude(_player.transform.position - position__Modified);

                isFiringDirectionSet = true;
            }

            //一定小時間ごとに発射する
            if (frameCount > shortFiringInterval)
            {
                frameCount = 0;

                Fire(firingDirection);

                firingCount++;
            }

            //一定数発射したら発射を終了する
            if (firingCount > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.RepeatedFire__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.FiringCount])
            {
                firingCount = 0;
                _time = 0;
                isFiringDirectionSet = false;
            }
        }
    }


    /// <summary>
    /// 弾を指定された速度で発射する
    /// </summary>
    /// <param name="bulletVelocity"></param>
    void Fire(Vector3 bulletVelocity)
    {
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
        bulletRigidbody2D.velocity = bulletVelocity;

        //ミサイルの場合は弾の向きを調整する
        if (normalEnemyType == NormalEnemyData_scr.normalEnemyType.Missile__MiddleDrone)
        {
            commonForBattleScenes.RotateToLookAt(bullet, transform.position, _player.transform.position);
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

            case NormalEnemyData_scr.normalEnemyType.Missile__MiddleDrone:
                enemyType = "Missile";
                break;

            case NormalEnemyData_scr.normalEnemyType.WidespreadBullet__MiddleDrone:
                enemyType = "WideSpreadBullet";
                break;

            case NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone:
                enemyType = "SpreadBullet";
                break;

            default:
                throw new System.Exception();
        }

        return "BattleScenes/EnemyFire__" + enemyType;
    }
}
