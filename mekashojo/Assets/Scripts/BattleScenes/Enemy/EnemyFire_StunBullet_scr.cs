using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire_StunBullet_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);

        DestroyLater();
    }
}
