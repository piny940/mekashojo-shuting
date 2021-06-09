using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu__ReturnButton_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
