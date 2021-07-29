using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton_scr : ButtonBaseImp
{
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    [SerializeField, Header("PauseScreeを入れる")] GameObject _pauseScreen;

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            _startCount.isStarting = true;
            _pauseScreen.SetActive(false);
        }
    }

}
