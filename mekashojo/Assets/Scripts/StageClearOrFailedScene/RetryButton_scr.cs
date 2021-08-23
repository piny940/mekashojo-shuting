using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton_scr : ButtonBase
{
    public void OnPush()
    {
        if (CanPush())
        {
            Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.sceneChangeManager.previousSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }
}
