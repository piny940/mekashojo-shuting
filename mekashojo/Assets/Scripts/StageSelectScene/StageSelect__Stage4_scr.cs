using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__Stage4_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.stage4;
        _stageSceneName = SceneChangeManager_scr.SceneNames.Stage4;
        this.Initialize();
    }
}
