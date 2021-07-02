using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton_scr : ButtonBaseImp
{
    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SaveDataManager_scr.saveDataManager.Initialize();

            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
        }
    }
}
