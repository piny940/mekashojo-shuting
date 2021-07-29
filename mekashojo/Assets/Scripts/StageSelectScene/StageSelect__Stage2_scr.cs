using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__Stage2_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.stage2;
        _stageSceneName = SceneChangeManager_scr.SceneNames.Stage2;
        this.Initialize();
    }
}
