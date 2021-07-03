using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__BeamMachineGun_scr : EquipmentSelectButtonBaseImp
{
    [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

    private void Start()
    {
        type = EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun;
        Initialize();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            Common_scr.common.audioSource.PlayOneShot(_pushSound);
            SelectedWeaponChanged();
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}