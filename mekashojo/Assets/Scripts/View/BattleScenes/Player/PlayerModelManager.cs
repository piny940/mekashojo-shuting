using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class PlayerModelManager : MonoBehaviour
    {
        [SerializeField, Header("PlayerModelを入れる")] private Animator _player;
        [SerializeField, Header("Cannonを入れる")] private GameObject _cannon;
        [SerializeField, Header("Laserを入れる")] private GameObject _laser;
        [SerializeField, Header("BeamMachineGunを入れる")] private GameObject _beamMachineGun;
        [SerializeField, Header("Balkanを入れる")] private GameObject _balkan;
        [SerializeField, Header("Missileを入れる")] private GameObject _missile;

        private GameObject _mainWeaponObject;
        private GameObject _subWeaponObject;
        private animationParameters _mainWeaponParameter;
        private animationParameters _subWeaponParameter;
        private Animator _mainWeaponAnimator;
        private Animator _subWeaponAnimator;

        private enum animationParameters
        {
            none,
            fire,
            selectCannon,
            selectLaser,
            selectBeamMachineGun,
            selectBalkan,
            selectMissile,
        }

        // Start is called before the first frame update
        void Start()
        {
            SetSelectedWeapons();
            SwitchWeapon(Controller.BattleScenesController.weaponManager.isMainSelected);

            Controller.BattleScenesController.weaponManager.OnIsMainSelectedChanged.AddListener(SwitchWeapon);

            Controller.BattleScenesController.cannon__Player.OnIsUsingCannonChanged.AddListener((bool isUsingCannon) =>
            {
                if (isUsingCannon)
                {
                    FireMainWeapon();
                }
            });

            Controller.BattleScenesController.laser__Player.OnIsUsingLaserChanged.AddListener((bool isUsingLaser) =>
            {
                if (isUsingLaser)
                {
                    FireMainWeapon();
                }
            });

            Controller.BattleScenesController.beamMachineGun__Player.OnBeamMachineGunNumberChanged.AddListener(FireMainWeapon);

            Controller.BattleScenesController.balkan__Player.OnBalkanNumberChanged.AddListener(FireSubWeapon);

            Controller.BattleScenesController.missile__Player.OnMissileNumberChanged.AddListener(FireSubWeapon);
        }

        private void SetSelectedWeapons()
        {
            // 一旦全て非アクティブにする
            _cannon.SetActive(false);
            _laser.SetActive(false);
            _beamMachineGun.SetActive(false);
            _balkan.SetActive(false);
            _missile.SetActive(false);

            // メイン武器の設定
            switch (Model.EquipmentData.equipmentData.selectedMainWeaponName)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                    _mainWeaponObject = _cannon;
                    _mainWeaponParameter = animationParameters.selectCannon;
                    break;

                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                    _mainWeaponObject = _laser;
                    _mainWeaponParameter = animationParameters.selectLaser;
                    break;

                case Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    _mainWeaponObject = _beamMachineGun;
                    _mainWeaponParameter = animationParameters.selectBeamMachineGun;
                    break;
            }
            _mainWeaponAnimator = _mainWeaponObject.GetComponent<Animator>();

            // サブ武器の設定
            switch (Model.EquipmentData.equipmentData.selectedSubWeaponName)
            {
                case Model.EquipmentData.equipmentType.SubWeapon__Balkan:
                    _subWeaponObject = _balkan;
                    _subWeaponParameter = animationParameters.selectBalkan;
                    break;

                case Model.EquipmentData.equipmentType.SubWeapon__Missile:
                    _subWeaponObject = _missile;
                    _subWeaponParameter = animationParameters.selectMissile;
                    break;
            }
            _subWeaponAnimator = _subWeaponObject.GetComponent<Animator>();
        }

        // メイン武器とサブ武器の切り替えを行う
        // アニメーションを開始させるため、Startメソッドでも呼ぶ必要がある
        private void SwitchWeapon(bool isMainSelected)
        {
            _mainWeaponObject.SetActive(isMainSelected);
            _subWeaponObject.SetActive(!isMainSelected);

            if (isMainSelected)
            {
                _mainWeaponAnimator.SetTrigger(_mainWeaponParameter.ToString());
                _player.SetTrigger(_mainWeaponParameter.ToString());
            }
            else
            {
                _subWeaponAnimator.SetTrigger(_subWeaponParameter.ToString());
                _player.SetTrigger(_subWeaponParameter.ToString());
            }
        }

        // メイン武器を発射する
        private void FireMainWeapon()
        {
            _player.SetTrigger(animationParameters.fire.ToString());
            _mainWeaponAnimator.SetTrigger(animationParameters.fire.ToString());
        }

        // サブ武器を発射する
        private void FireSubWeapon()
        {
            _player.SetTrigger(animationParameters.fire.ToString());
            _subWeaponAnimator.SetTrigger(animationParameters.fire.ToString());
        }
    }
}
