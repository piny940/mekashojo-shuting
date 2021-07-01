using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SpreadBullet_scr : EnemyBaseImp
{
    float _time;
    int _firingCount;
    List<Vector3> _fireDirections = new List<Vector3>();
    

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        _time = Random.Range(0, NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.FiringInterval]);

        _firingCount = (int)Mathf.Floor((int)NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.FiringCount]);

        //弾を発射する方向を計算
        for(int i = 0; i < _firingCount; i++)
        {
            _fireDirections.Add(new Vector3(Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / _firingCount), Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / _firingCount), 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        commonForBattleScenes.Pause(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack();
    }

    void Attack()
    {
        _time += Time.deltaTime;

        if (_time > NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.FiringInterval])
        {
            _time = 0;

            for(int i = 0; i < _firingCount; i++)
            {
                //弾の生成
                GameObject bullet = Instantiate((GameObject)Resources.Load("BattleScenes/EnemyFire__SpreadBullet"), transform.position, Quaternion.identity);

                //コンポーネントの取得
                Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();

                //null安全性を確保
                if (bulletRigidbody2D == null)
                {
                    throw new System.Exception();
                }

                //弾の速度の設定
                bulletRigidbody2D.velocity = _fireDirections[i] * NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed];

            }
        }
    }
}
