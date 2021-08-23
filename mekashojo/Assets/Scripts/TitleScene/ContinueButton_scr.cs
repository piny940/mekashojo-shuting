using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton_scr : ButtonBase
{
    [SerializeField, Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;
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
            //セーブデータがなかった場合
            if (!Model.SaveDataManager.saveDataManager.Load())
            {
                SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_noSaveDataSound);
                _noSaveDataScreen.SetActive(true);
                return;
            }

            //セーブデータがあった場合
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_existSaveDataSound);
            Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.MenuScene);
        }
    }
}
