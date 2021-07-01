using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideBeam__Enemy_scr : EnemyFireBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.WideBeam__MiddleDrone;

        Initialize();
    }

}
