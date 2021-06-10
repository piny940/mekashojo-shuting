using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect__ReturnButton_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
