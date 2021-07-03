using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton_scr : ButtonBaseImp
{
    [SerializeField,Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;
    [SerializeField, Header("セーブデータがなかったときのボタンのサウンド")] AudioClip _noSaveDataSound;
    [SerializeField, Header("セーブデータがあったときのボタンのサウンド")] AudioClip _existSaveDataSound;

    // Start is called before the first frame update
    void Start()
    {
        _noSaveDataScreen.SetActive(false);
    }

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            //セーブデータを読み込む
            SaveDataManager_scr.saveDataManager.LoadData();

            //セーブデータがなかった場合
            if (SaveDataManager_scr.saveDataManager.haveNoSaveData)
            {
                Common_scr.common.audioSource.PlayOneShot(_noSaveDataSound);
                _noSaveDataScreen.SetActive(true);
                SaveDataManager_scr.saveDataManager.haveNoSaveData = false;
                return;
            }

            //セーブデータがあった場合
            Common_scr.common.audioSource.PlayOneShot(_existSaveDataSound);
            SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
        }
    }
}
