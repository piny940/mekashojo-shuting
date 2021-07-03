using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect__ReturnButton_scr : ButtonBaseImp
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    public void OnPush()
    {
        if (CanPush())
        {
            Common_scr.common.audioSource.PlayOneShot(_pushSound);
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
