using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReallyQuitBattleButton_scr : MonoBehaviour
{
    public void OnPush()
    {
        SaveDataManager_scr.saveDataManager.SaveData();
        SceneManager.LoadScene("MenuScene");
    }
}
