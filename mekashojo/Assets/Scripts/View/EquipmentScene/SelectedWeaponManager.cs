using UnityEngine;

namespace View
{
    public class SelectedWeaponManager : MonoBehaviour
    {
        [SerializeField, Header("EquipmentSelectButton__Cannon__Selected")] private EquipmentSelectButton__Cannon _equipmentSelectButton__Cannon__Selected;
        [SerializeField, Header("EquipmentSelectButton__Cannon__UnSelected")] private EquipmentSelectButton__Cannon _equipmentSelectButton__Cannon__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__Laser__Selected")] private EquipmentSelectButton__Laser _equipmentSelectButton__Laser__Selected;
        [SerializeField, Header("EquipmentSelectButton__Laser__UnSelected")] private EquipmentSelectButton__Laser _equipmentSelectButton__Laser__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__BeamMachineGun__Selected")] private EquipmentSelectButton__BeamMachineGun _equipmentSelectButton__BeamMachineGun__Selected;
        [SerializeField, Header("EquipmentSelectButton__BeamMachineGun__UnSelected")] private EquipmentSelectButton__BeamMachineGun _equipmentSelectButton__BeamMachineGun__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__Balkan__Selected")] private EquipmentSelectButton__Balkan _equipmentSelectButton__Balkan__Selected;
        [SerializeField, Header("EquipmentSelectButton__Balkan__UnSelected")] private EquipmentSelectButton__Balkan _equipmentSelectButton__Balkan__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__Missile__Selected")] private EquipmentSelectButton__Missile _equipmentSelectButton__Missile__Selected;
        [SerializeField, Header("EquipmentSelectButton__Missile__UnSelected")] private EquipmentSelectButton__Missile _equipmentSelectButton__Missile__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__Bomb__Selected")] private EquipmentSelectButton__Bomb _equipmentSelectButton__Bomb__Selected;
        [SerializeField, Header("EquipmentSelectButton__HeavyShield__Selected")] private EquipmentSelectButton__HeavyShield _equipmentSelectButton__HeavyShield__Selected;
        [SerializeField, Header("EquipmentSelectButton__HeavyShield__UnSelected")] private EquipmentSelectButton__HeavyShield _equipmentSelectButton__HeavyShield__UnSelected;
        [SerializeField, Header("EquipmentSelectButton__LightShield__Selected")] private EquipmentSelectButton__LightShield _equipmentSelectButton__LightShield__Selected;
        [SerializeField, Header("EquipmentSelectButton__LightShield__UnSelected")] private EquipmentSelectButton__LightShield _equipmentSelectButton__LightShield__UnSelected;

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

        public void NotifySelectedWeaponChanged(Model.EquipmentData.equipmentType type)
        {
            switch (type)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                case Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    ChangeMainWeapon(Model.EquipmentData.equipmentData.selectedMainWeaponName, type);
                    break;

                case Model.EquipmentData.equipmentType.SubWeapon__Balkan:
                case Model.EquipmentData.equipmentType.SubWeapon__Missile:
                    ChangeSubWeapon(Model.EquipmentData.equipmentData.selectedSubWeaponName, type);
                    break;

                case Model.EquipmentData.equipmentType.Shield__Heavy:
                case Model.EquipmentData.equipmentType.Shield__Light:
                    ChangeShield(Model.EquipmentData.equipmentData.selectedShieldName, type);
                    break;

                default:
                    break;
            }
        }

        private void ChangeMainWeapon(Model.EquipmentData.equipmentType oldWeapon, Model.EquipmentData.equipmentType newWeapon)
        {
            switch (oldWeapon)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                    _equipmentSelectButton__Cannon__Selected.isVisible = false;
                    _equipmentSelectButton__Cannon__UnSelected.isVisible = true;
                    break;
                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                    _equipmentSelectButton__Laser__Selected.isVisible = false;
                    _equipmentSelectButton__Laser__UnSelected.isVisible = true;
                    break;
                case Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    _equipmentSelectButton__BeamMachineGun__Selected.isVisible = false;
                    _equipmentSelectButton__BeamMachineGun__UnSelected.isVisible = true;
                    break;
            }

            switch (newWeapon)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                    _equipmentSelectButton__Cannon__Selected.isVisible = true;
                    _equipmentSelectButton__Cannon__UnSelected.isVisible = false;
                    break;
                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                    _equipmentSelectButton__Laser__Selected.isVisible = true;
                    _equipmentSelectButton__Laser__UnSelected.isVisible = false;
                    break;
                case Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    _equipmentSelectButton__BeamMachineGun__Selected.isVisible = true;
                    _equipmentSelectButton__BeamMachineGun__UnSelected.isVisible = false;
                    break;
            }

            Model.EquipmentData.equipmentData.selectedMainWeaponName = newWeapon;
        }

        private void ChangeSubWeapon(Model.EquipmentData.equipmentType oldWeapon, Model.EquipmentData.equipmentType newWeapon)
        {
            switch (oldWeapon)
            {
                case Model.EquipmentData.equipmentType.SubWeapon__Balkan:
                    _equipmentSelectButton__Balkan__Selected.isVisible = false;
                    _equipmentSelectButton__Balkan__UnSelected.isVisible = true;
                    break;
                case Model.EquipmentData.equipmentType.SubWeapon__Missile:
                    _equipmentSelectButton__Missile__Selected.isVisible = false;
                    _equipmentSelectButton__Missile__UnSelected.isVisible = true;
                    break;
            }

            switch (newWeapon)
            {
                case Model.EquipmentData.equipmentType.SubWeapon__Balkan:
                    _equipmentSelectButton__Balkan__Selected.isVisible = true;
                    _equipmentSelectButton__Balkan__UnSelected.isVisible = false;
                    break;
                case Model.EquipmentData.equipmentType.SubWeapon__Missile:
                    _equipmentSelectButton__Missile__Selected.isVisible = true;
                    _equipmentSelectButton__Missile__UnSelected.isVisible = false;
                    break;
            }

            Model.EquipmentData.equipmentData.selectedSubWeaponName = newWeapon;
        }

        private void ChangeShield(Model.EquipmentData.equipmentType oldWeapon, Model.EquipmentData.equipmentType newWeapon)
        {
            switch (oldWeapon)
            {
                case Model.EquipmentData.equipmentType.Shield__Heavy:
                    _equipmentSelectButton__HeavyShield__Selected.isVisible = false;
                    _equipmentSelectButton__HeavyShield__UnSelected.isVisible = true;
                    break;
                case Model.EquipmentData.equipmentType.Shield__Light:
                    _equipmentSelectButton__LightShield__Selected.isVisible = false;
                    _equipmentSelectButton__LightShield__UnSelected.isVisible = true;
                    break;
            }

            switch (newWeapon)
            {
                case Model.EquipmentData.equipmentType.Shield__Heavy:
                    _equipmentSelectButton__HeavyShield__Selected.isVisible = true;
                    _equipmentSelectButton__HeavyShield__UnSelected.isVisible = false;
                    break;
                case Model.EquipmentData.equipmentType.Shield__Light:
                    _equipmentSelectButton__LightShield__Selected.isVisible = true;
                    _equipmentSelectButton__LightShield__UnSelected.isVisible = false;
                    break;
            }

            Model.EquipmentData.equipmentData.selectedShieldName = newWeapon;
        }
    }
}
