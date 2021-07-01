using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton_scr : MonoBehaviour
{
    [SerializeField,Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;

    // Start is called before the first frame update
    void Start()
    {
        _noSaveDataScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush()
    {
        //セーブデータを読み込む
        SaveDataManager_scr.saveDataManager.LoadData();

        //セーブデータがなかった場合
        if (SaveDataManager_scr.saveDataManager.haveNoSaveData)
        {
            _noSaveDataScreen.SetActive(true);
            SaveDataManager_scr.saveDataManager.haveNoSaveData = false;
            return;
        }

        //セーブデータがあった場合
        SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);


    }
}
