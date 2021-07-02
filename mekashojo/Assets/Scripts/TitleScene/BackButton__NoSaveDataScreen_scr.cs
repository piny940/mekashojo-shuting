using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton__NoSaveDataScreen_scr : ButtonBaseImp
{
    [SerializeField, Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;

    private void Update()
    {
        ButtonUpdate();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            _noSaveDataScreen.SetActive(false);
        }
    }
}
