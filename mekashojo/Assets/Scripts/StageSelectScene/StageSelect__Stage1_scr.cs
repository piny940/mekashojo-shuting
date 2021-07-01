using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__Stage1_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.stage1;
        _stageSceneName = SceneChangeManager_scr.SceneNames.Stage1;
        this.Initialize();
    }
}
