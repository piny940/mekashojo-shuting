using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStageButton_scr : ButtonBaseImp
{
    [SerializeField, Header("QuitBattleCheckScreenを入れる")] GameObject _quitBattleCheckScreen;

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            _quitBattleCheckScreen.SetActive(false);
        }
    }
}
