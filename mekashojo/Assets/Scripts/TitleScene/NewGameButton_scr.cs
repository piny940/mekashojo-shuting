using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton_scr : ButtonBase
{
    [SerializeField, Header("押したときになる音")] AudioClip _clickSound;

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_clickSound);

            Model.SaveDataManager.saveDataManager.Initialize();

            Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.MenuScene);
        }
    }
}
