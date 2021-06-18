using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWeaponManager_scr : MonoBehaviour
{
    [SerializeField, Header("EquipmentSelectButton__Cannon__Selected")] private EquipmentSelectButton__Cannon_scr _equipmentSelectButton__Cannon__Selected;
    [SerializeField, Header("EquipmentSelectButton__Cannon__UnSelected")] private EquipmentSelectButton__Cannon_scr _equipmentSelectButton__Cannon__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__Laser__Selected")] private EquipmentSelectButton__Laser_scr _equipmentSelectButton__Laser__Selected;
    [SerializeField, Header("EquipmentSelectButton__Laser__UnSelected")] private EquipmentSelectButton__Laser_scr _equipmentSelectButton__Laser__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__BeamMachineGun__Selected")] private EquipmentSelectButton__BeamMachineGun_scr _equipmentSelectButton__BeamMachineGun__Selected;
    [SerializeField, Header("EquipmentSelectButton__BeamMachineGun__UnSelected")] private EquipmentSelectButton__BeamMachineGun_scr _equipmentSelectButton__BeamMachineGun__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__Balkan__Selected")] private EquipmentSelectButton__Balkan_scr _equipmentSelectButton__Balkan__Selected;
    [SerializeField, Header("EquipmentSelectButton__Balkan__UnSelected")] private EquipmentSelectButton__Balkan_scr _equipmentSelectButton__Balkan__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__Missile__Selected")] private EquipmentSelectButton__Missile_scr _equipmentSelectButton__Missile__Selected;
    [SerializeField, Header("EquipmentSelectButton__Missile__UnSelected")] private EquipmentSelectButton__Missile_scr _equipmentSelectButton__Missile__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__Bomb__Selected")] private EquipmentSelectButton__Bomb_scr _equipmentSelectButton__Bomb__Selected;
    [SerializeField, Header("EquipmentSelectButton__HeavyShield__Selected")] private EquipmentSelectButton__HeavyShield_scr _equipmentSelectButton__HeavyShield__Selected;
    [SerializeField, Header("EquipmentSelectButton__HeavyShield__UnSelected")] private EquipmentSelectButton__HeavyShield_scr _equipmentSelectButton__HeavyShield__UnSelected;
    [SerializeField, Header("EquipmentSelectButton__LightShield__Selected")] private EquipmentSelectButton__LightShield_scr _equipmentSelectButton__LightShield__Selected;
    [SerializeField, Header("EquipmentSelectButton__LightShield__UnSelected")] private EquipmentSelectButton__LightShield_scr _equipmentSelectButton__LightShield__UnSelected;

    private void Awake()
    {
        _equipmentSelectButton__Cannon__Selected.isVisible = false;
        _equipmentSelectButton__Laser__Selected.isVisible = false;
        _equipmentSelectButton__BeamMachineGun__Selected.isVisible = false;
        _equipmentSelectButton__Balkan__Selected.isVisible = false;
        _equipmentSelectButton__Missile__Selected.isVisible = false;
        _equipmentSelectButton__HeavyShield__Selected.isVisible = false;
        _equipmentSelectButton__LightShield__Selected.isVisible = false;
    }

    public void NotifySelectedWeaponChanged(EquipmentData_scr.equipmentType type)
    {
        switch (type)
        {
            case EquipmentData_scr.equipmentType.MainWeapon__Cannon:
            case EquipmentData_scr.equipmentType.MainWeapon__Laser:
            case EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                ChangeMainWeapon(EquipmentData_scr.equipmentData.selectedMainWeaponName, type);
                break;

            case EquipmentData_scr.equipmentType.SubWeapon__Balkan:
            case EquipmentData_scr.equipmentType.SubWeapon__Missile:
                ChangeSubWeapon(EquipmentData_scr.equipmentData.selectedSubWeaponName, type);
                break;

            case EquipmentData_scr.equipmentType.Shield__Heavy:
            case EquipmentData_scr.equipmentType.Shield__Light:
                ChangeShield(EquipmentData_scr.equipmentData.selectedShieldName, type);
                break;

            default:
                break;
        }
    }

    private void ChangeMainWeapon(EquipmentData_scr.equipmentType oldWeapon, EquipmentData_scr.equipmentType newWeapon)
    {
        switch (oldWeapon)
        {
            case EquipmentData_scr.equipmentType.MainWeapon__Cannon:
                _equipmentSelectButton__Cannon__Selected.isVisible = false;
                _equipmentSelectButton__Cannon__UnSelected.isVisible = true;
                break;
            case EquipmentData_scr.equipmentType.MainWeapon__Laser:
                _equipmentSelectButton__Laser__Selected.isVisible = false;
                _equipmentSelectButton__Laser__UnSelected.isVisible = true;
                break;
            case EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                _equipmentSelectButton__BeamMachineGun__Selected.isVisible = false;
                _equipmentSelectButton__BeamMachineGun__UnSelected.isVisible = true;
                break;
        }

        switch (newWeapon)
        {
            case EquipmentData_scr.equipmentType.MainWeapon__Cannon:
                _equipmentSelectButton__Cannon__Selected.isVisible = true;
                _equipmentSelectButton__Cannon__UnSelected.isVisible = false;
                break;
            case EquipmentData_scr.equipmentType.MainWeapon__Laser:
                _equipmentSelectButton__Laser__Selected.isVisible = true;
                _equipmentSelectButton__Laser__UnSelected.isVisible = false;
                break;
            case EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                _equipmentSelectButton__BeamMachineGun__Selected.isVisible = true;
                _equipmentSelectButton__BeamMachineGun__UnSelected.isVisible = false;
                break;
        }

        EquipmentData_scr.equipmentData.selectedMainWeaponName = newWeapon;
    }

    private void ChangeSubWeapon(EquipmentData_scr.equipmentType oldWeapon, EquipmentData_scr.equipmentType newWeapon)
    {
        switch (oldWeapon)
        {
            case EquipmentData_scr.equipmentType.SubWeapon__Balkan:
                _equipmentSelectButton__Balkan__Selected.isVisible = false;
                _equipmentSelectButton__Balkan__UnSelected.isVisible = true;
                break;
            case EquipmentData_scr.equipmentType.SubWeapon__Missile:
                _equipmentSelectButton__Missile__Selected.isVisible = false;
                _equipmentSelectButton__Missile__UnSelected.isVisible = true;
                break;
        }

        switch (newWeapon)
        {
            case EquipmentData_scr.equipmentType.SubWeapon__Balkan:
                _equipmentSelectButton__Balkan__Selected.isVisible = true;
                _equipmentSelectButton__Balkan__UnSelected.isVisible = false;
                break;
            case EquipmentData_scr.equipmentType.SubWeapon__Missile:
                _equipmentSelectButton__Missile__Selected.isVisible = true;
                _equipmentSelectButton__Missile__UnSelected.isVisible = false;
                break;
        }

        EquipmentData_scr.equipmentData.selectedSubWeaponName = newWeapon;
    }

    private void ChangeShield(EquipmentData_scr.equipmentType oldWeapon, EquipmentData_scr.equipmentType newWeapon)
    {
        switch (oldWeapon)
        {
            case EquipmentData_scr.equipmentType.Shield__Heavy:
                _equipmentSelectButton__HeavyShield__Selected.isVisible = false;
                _equipmentSelectButton__HeavyShield__UnSelected.isVisible = true;
                break;
            case EquipmentData_scr.equipmentType.Shield__Light:
                _equipmentSelectButton__LightShield__Selected.isVisible = false;
                _equipmentSelectButton__LightShield__UnSelected.isVisible = true;
                break;
        }

        switch (newWeapon)
        {
            case EquipmentData_scr.equipmentType.Shield__Heavy:
                _equipmentSelectButton__HeavyShield__Selected.isVisible = true;
                _equipmentSelectButton__HeavyShield__UnSelected.isVisible = false;
                break;
            case EquipmentData_scr.equipmentType.Shield__Light:
                _equipmentSelectButton__LightShield__Selected.isVisible = true;
                _equipmentSelectButton__LightShield__UnSelected.isVisible = false;
                break;
        }

        EquipmentData_scr.equipmentData.selectedShieldName = newWeapon;
    }
}
