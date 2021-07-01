using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu__Story_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.StoryScene);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
