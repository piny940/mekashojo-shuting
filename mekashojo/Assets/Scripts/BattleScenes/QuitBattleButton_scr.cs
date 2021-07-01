using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBattleButton_scr : ButtonBaseImp
{
    [SerializeField, Header("QuitBattleCheckScreenを入れる")] GameObject _quitBattleCheckScreen;

    private void Start()
    {
        _quitBattleCheckScreen.SetActive(false);
    }

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            _quitBattleCheckScreen.SetActive(true);
        }
    }
}
