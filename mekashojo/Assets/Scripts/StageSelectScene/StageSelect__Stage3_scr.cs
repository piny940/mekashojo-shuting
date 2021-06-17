using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__Stage3_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.stage3;
        _stageSceneName = "Stage3";
        this.Initialize();
    }
}
