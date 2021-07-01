using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu__StageSelect_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.StageSelectScene);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
