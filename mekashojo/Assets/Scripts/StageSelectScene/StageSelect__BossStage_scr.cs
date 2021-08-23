using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__BossStage_scr : StageSelectButtonBaseImp
{
    private void Start()
    {
        _stageName = ProgressData_scr.stageName.lastStage;
        _stageSceneName = Model.SceneChangeManager.SceneNames.LastStage;
        this.Initialize();
    }
}
