using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReallyQuitBattleButton_scr : ButtonBaseImp
{
    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SaveDataManager_scr.saveDataManager.SaveData();
            SceneManager.LoadScene("MenuScene");
        }
    }
}
