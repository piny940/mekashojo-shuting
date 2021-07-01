using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.StageSelectScene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }
}
