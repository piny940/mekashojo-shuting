using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__Balkan_scr : EquipmentSelectButtonBaseImp
{
    private void Start()
    {
        type = EquipmentData_scr.equipmentType.SubWeapon__Balkan;
        Initialize();
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SelectedWeaponChanged();
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}