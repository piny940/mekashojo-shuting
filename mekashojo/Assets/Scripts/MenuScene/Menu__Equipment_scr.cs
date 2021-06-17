using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu__Equipment_scr : ButtonBaseImp
{
    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene("EquipmentScene");
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
