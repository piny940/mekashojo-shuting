using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBattleButton_scr : MonoBehaviour
{
    [SerializeField, Header("QuitBattleCheckScreenを入れる")] GameObject _quitBattleCheckScreen;

    private void Start()
    {
        _quitBattleCheckScreen.SetActive(false);
    }

    public void OnPush()
    {
        _quitBattleCheckScreen.SetActive(true);
    }
}
