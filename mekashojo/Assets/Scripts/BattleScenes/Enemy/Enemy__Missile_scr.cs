using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__Missile_scr : Enemy__BulletBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone;

        BulletEnemyInitialize();
    }
}
