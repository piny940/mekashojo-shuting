using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStageButton_scr : MonoBehaviour
{
    [SerializeField, Header("QuitBattleCheckScreenを入れる")] GameObject _quitBattleCheckScreen;

    public void OnPush()
    {
        _quitBattleCheckScreen.SetActive(false);
    }
}
