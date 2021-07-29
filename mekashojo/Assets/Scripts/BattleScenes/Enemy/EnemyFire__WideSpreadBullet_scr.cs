using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire__WideSpreadBullet_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.WidespreadBullet__MiddleDrone;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);

        DestroyLater();
    }
}
