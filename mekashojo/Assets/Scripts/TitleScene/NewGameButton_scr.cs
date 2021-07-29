using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton_scr : ButtonBaseImp
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

            SaveDataManager_scr.saveDataManager.Initialize();

            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
        }
    }
}
