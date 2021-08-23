using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment__ReturnButton_scr : ButtonBase
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_pushSound);
            Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.MenuScene);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
