using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire__WideBeam_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        commonForBattleScenes.Pause(rigidbody2D, ref isPausing, ref savedVelocity);
    }

}
