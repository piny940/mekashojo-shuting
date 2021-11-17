using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class PreviewImageModel : MonoBehaviour
    {
        [SerializeField, Header("Player__Cannonを入れる")] private GameObject _cannon;
        [SerializeField, Header("Player__Laserを入れる")] private GameObject _laser;
        [SerializeField, Header("Player__BeamMachineGunを入れる")] private GameObject _beamMachineGun;
        [SerializeField, Header("Player__Balkanを入れる")] private GameObject _balkan;
        [SerializeField, Header("Player__Missileを入れる")] private GameObject _missile;
        [SerializeField, Header("Player__Bombを入れる")] private GameObject _bomb;
        [SerializeField, Header("Player__HeavyShieldを入れる")] private GameObject _heavyShield;
        [SerializeField, Header("Player__LightShieldを入れる")] private GameObject _lightShield;

        private Model.EquipmentData.equipmentType _modelType;

        private Model.EquipmentData.equipmentType _currentModelType;

        private Dictionary<Model.EquipmentData.equipmentType, GameObject> _playerModels;

        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }

        public Model.EquipmentData.equipmentType modelType
        {
            get { return _modelType; }
            set
            {
                _modelType = value;
                UpdateModelType(value);
            }
        }

        private void Awake()
        {
            _playerModels = new Dictionary<Model.EquipmentData.equipmentType, GameObject>()
            {
                { Model.EquipmentData.equipmentType.MainWeapon__Cannon, _cannon },
                { Model.EquipmentData.equipmentType.MainWeapon__Laser, _laser },
                { Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun, _beamMachineGun },
                { Model.EquipmentData.equipmentType.SubWeapon__Balkan, _balkan },
                { Model.EquipmentData.equipmentType.SubWeapon__Missile, _missile },
                { Model.EquipmentData.equipmentType.Bomb, _bomb },
                { Model.EquipmentData.equipmentType.Shield__Heavy, _heavyShield },
                { Model.EquipmentData.equipmentType.Shield__Light, _lightShield },
            };
        }

        private void Start()
        {
            _cannon.SetActive(false);
            _laser.SetActive(false);
            _beamMachineGun.SetActive(false);
            _balkan.SetActive(false);
            _missile.SetActive(false);
            _bomb.SetActive(false);
            _heavyShield.SetActive(false);
            _lightShield.SetActive(false);
        }

        private void UpdateModelType(Model.EquipmentData.equipmentType type)
        {
            // Shieldのオブジェクトは、typeがそのオブジェクトのタイプと一致した場合のみ表示する
            _heavyShield.SetActive(type == Model.EquipmentData.equipmentType.Shield__Heavy);
            _lightShield.SetActive(type == Model.EquipmentData.equipmentType.Shield__Light);

            // typeがShield系の場合は終了する
            if (type == Model.EquipmentData.equipmentType.Shield__Heavy ||
                type == Model.EquipmentData.equipmentType.Shield__Light)
            {
                return;
            }

            _playerModels[_currentModelType].SetActive(false);
            _playerModels[type].SetActive(true);

            _currentModelType = type;
        }
    }
}
