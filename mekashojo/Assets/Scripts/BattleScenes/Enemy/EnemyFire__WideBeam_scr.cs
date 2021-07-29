using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire__WideBeam_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.WideBeam__MiddleDrone;

        Initialize();
    }

    private void Update()
    {
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);
    }

}
