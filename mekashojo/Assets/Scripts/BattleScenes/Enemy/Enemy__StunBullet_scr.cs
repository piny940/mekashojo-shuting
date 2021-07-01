using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__StunBullet_scr : Enemy__BulletBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone;

        BulletEnemyInitialize();
    }
}
