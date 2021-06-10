using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__Stage2_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.stage2;
        _stageSceneName = "Stage2";
        this.Initialize();
    }
}
