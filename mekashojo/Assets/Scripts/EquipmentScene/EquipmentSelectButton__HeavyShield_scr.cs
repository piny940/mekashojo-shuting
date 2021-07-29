using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__HeavyShield_scr : EquipmentSelectButtonBaseImp
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    private void Start()
    {
        type = EquipmentData_scr.equipmentType.Shield__Heavy;
        Initialize();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SEPlayer_scr.sePlayer.audioSource.PlayOneShot(_pushSound);
            SelectedWeaponChanged();
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
