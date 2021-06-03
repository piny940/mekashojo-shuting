using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton__NoSaveDataScreen_scr : MonoBehaviour
{
    [SerializeField, Header("NoSaveDataScreenを入れる")] GameObject _noSaveDataScreen;

    public void OnPush()
    {
        _noSaveDataScreen.SetActive(false);
    }
}
