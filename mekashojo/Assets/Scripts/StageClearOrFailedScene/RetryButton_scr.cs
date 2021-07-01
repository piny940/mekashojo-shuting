using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.sceneChangeManager.previousSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }
}
