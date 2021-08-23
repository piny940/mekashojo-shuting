using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu__Settings_scr : ButtonBase
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_pushSound);
            //具体的な処理はまだ
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUpdate();
    }
}
