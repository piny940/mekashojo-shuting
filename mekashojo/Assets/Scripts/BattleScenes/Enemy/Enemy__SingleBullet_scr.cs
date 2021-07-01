using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SingleBullet_scr : Enemy__BulletBaseImp
{
    private void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.SingleBullet__SmallDrone;

        BulletEnemyInitialize();
    }
}
