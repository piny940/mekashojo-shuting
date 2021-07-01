using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__FastBullet_scr : Enemy__BulletBaseImp
{
    private void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.FastBullet__SmallDrone;

        BulletEnemyInitialize();

    }
}
