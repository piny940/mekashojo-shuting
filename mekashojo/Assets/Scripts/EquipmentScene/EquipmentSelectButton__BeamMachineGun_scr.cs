using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton__BeamMachineGun_scr : EquipmentSelectButtonBaseImp
{
    private void Start()
    {
        type = EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun;
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