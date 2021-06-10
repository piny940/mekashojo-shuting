using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButtonBaseImp : ButtonBaseImp
{
    [SerializeField, Header("StageDescriptionsを入れる")] private StageDescriptions_scr _stageDescriptions;
    [SerializeField, Header("StartButtonを入れる")]  private StartButton_scr _startButton;

    protected ProgressData_scr.stageName _stageName;
    // ステージのシーン名
    protected string _stageSceneName;

    protected void Initialize()
    {
        GetComponentInChildren<Text>().text = ProgressData_scr.progressData.stageDisplayName[_stageName];
    }

    public void OnPush()
    {
        if (CanPush())
        {
            _stageDescriptions.text = ProgressData_scr.progressData.stageDescriptions[_stageName];
            _startButton.selectingStageName = _stageSceneName;
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
