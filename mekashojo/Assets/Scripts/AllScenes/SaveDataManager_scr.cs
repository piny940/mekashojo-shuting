using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager_scr : MonoBehaviour
{
    public static SaveDataManager_scr saveDataManager;

    //シングルトン
    private void Awake()
    {
        if (saveDataManager == null)
        {
            saveDataManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
