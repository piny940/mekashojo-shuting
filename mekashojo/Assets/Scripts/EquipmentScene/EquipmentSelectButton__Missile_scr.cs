using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__Missile_scr : EquipmentSelectButtonBaseImp
{
    private void Start()
    {
        type = EquipmentData_scr.equipmentType.SubWeapon__Missile;
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