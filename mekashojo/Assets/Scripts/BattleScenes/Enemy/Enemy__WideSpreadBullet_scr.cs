using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__WideSpreadBullet_scr : Enemy__BulletBaseImp
{
    int _firingCount;
    List<Vector3> _fireDirections = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.WidespreadBullet__MiddleDrone;

        BulletEnemyInitialize();

        _firingCount = (int)NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.WidespreadBullet__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.FiringCount];

        //弾を発射する方向を計算
        for (int i = 0; i < _firingCount; i++)
        {
            _fireDirections.Add(new Vector3(Mathf.Cos(2 * Mathf.PI * i / _firingCount), Mathf.Sin(2 * Mathf.PI * i / _firingCount), 0) * NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.WidespreadBullet__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);

        SetVelocity();

        Attack(_fireDirections);

        DestroyLater();
    }
}
