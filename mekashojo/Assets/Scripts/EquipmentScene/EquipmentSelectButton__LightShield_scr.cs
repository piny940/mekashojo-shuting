using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__LightShield_scr : EquipmentSelectButtonBaseImp
{
    private void Start()
    {
        type = EquipmentData_scr.equipmentType.Shield__Light;
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