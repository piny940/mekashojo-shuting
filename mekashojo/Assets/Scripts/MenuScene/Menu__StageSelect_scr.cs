using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu__StageSelect_scr : ButtonBaseImp
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_pushSound);
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.StageSelectScene);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
