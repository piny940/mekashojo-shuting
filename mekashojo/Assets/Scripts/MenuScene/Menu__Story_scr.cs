using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu__Story_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene("StoryScene");
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
