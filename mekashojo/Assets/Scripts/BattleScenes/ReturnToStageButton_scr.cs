using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStageButton_scr : MonoBehaviour
{
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    [SerializeField, Header("PauseScreeを入れる")] GameObject _pauseScreen;

    public void OnPush()
    {
        _startCount.isStarting = true;
        _pauseScreen.SetActive(false);
    }
    
}
