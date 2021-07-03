using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButtonBaseImp : ButtonBaseImp
{
    [SerializeField, Header("StageDescriptionsを入れる")] private StageDescriptions_scr _stageDescriptions;
    [SerializeField, Header("StartButtonを入れる")]  private StartButton_scr _startButton;
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    protected ProgressData_scr.stageName _stageName;
    // ステージのシーン名
    protected SceneChangeManager_scr.SceneNames _stageSceneName;

    protected void Initialize()
    {
        GetComponentInChildren<Text>().text = ProgressData_scr.progressData.stageDisplayName[_stageName];
    }

    public void OnPush()
    {
        if (CanPush())
        {
            Common_scr.common.audioSource.PlayOneShot(_pushSound);
            _stageDescriptions.text = ProgressData_scr.progressData.stageDescriptions[_stageName];
            _startButton.selectingStageName = _stageSceneName;
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
