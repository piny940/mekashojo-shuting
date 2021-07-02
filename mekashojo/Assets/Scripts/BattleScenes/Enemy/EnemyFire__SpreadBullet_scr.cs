using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire__SpreadBullet_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.SpreadBullet__SmallDrone;

        Initialize();
    }

    private void Update()
    {
        DestroyLater();
    }
}
