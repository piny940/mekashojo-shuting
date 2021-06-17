using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu__StageSelect_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
