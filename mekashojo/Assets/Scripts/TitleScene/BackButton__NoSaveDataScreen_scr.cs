using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton__NoSaveDataScreen_scr : ButtonBaseImp
{
    [SerializeField, Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;
    [SerializeField, Header("ボタンを押した時のサウンド")] AudioClip _pushSound;

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_pushSound);
            _noSaveDataScreen.SetActive(false);
        }
    }
}
