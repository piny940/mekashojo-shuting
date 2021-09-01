using System;
using UnityEngine.Events;

namespace Model
{
    public class WeaponManager
    {
        public Action MainProceedAttack;

        public Action SubProceedAttack;

        public UnityEvent<bool> OnIsMainSelectedChanged = new UnityEvent<bool>();

        private bool _isMainSelected = true;

        public bool isSwitchingWeapon;

        public bool isMainSelected
        {
            get { return _isMainSelected; }
            set
            {
                _isMainSelected = value;
                OnIsMainSelectedChanged?.Invoke(_isMainSelected);
            }
        }

        private PauseManager _pauseManager;
        private PlayerPositionManager _playerPositionManager;
        private Controller.WeaponInstances _weaponInstances;

        public WeaponManager(PauseManager pauseManager, PlayerPositionManager playerPositionManager, Controller.WeaponInstances weaponInstances)
        {
            _pauseManager = pauseManager;
            _playerPositionManager = playerPositionManager;
            _weaponInstances = weaponInstances;

            SetWeapons();
        }


        //選択中の武器のExecuteをProceedAttackに渡す
        private void SetWeapons()
        {
            switch (EquipmentData.equipmentData.selectedMainWeaponName)
            {
                case EquipmentData.equipmentType.MainWeapon__Cannon:
                    MainProceedAttack = _weaponInstances.cannon__Player.Execute;
                    break;

                case EquipmentData.equipmentType.MainWeapon__Laser:
                    MainProceedAttack = _weaponInstances.laser__Player.Execute;
                    break;

                case EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    MainProceedAttack = _weaponInstances.beamMachineGun__Player.Execute;
                    break;

                default:
                    break;
            }

            switch (EquipmentData.equipmentData.selectedSubWeaponName)
            {
                case EquipmentData.equipmentType.SubWeapon__Balkan:
                    SubProceedAttack = _weaponInstances.balkan__Player.Execute;
                    break;

                case EquipmentData.equipmentType.SubWeapon__Missile:
                    SubProceedAttack = _weaponInstances.missile__Player.Execute;
                    break;

                default:
                    break;
            }
        }

        public void RunEveryFrame()
        {
            SwitchWeapon();
            ProceedAttack();
        }

        private void SwitchWeapon()
        {
            //ゲームが進行中ではない　または　スタン中の時は武器の切り替えはできない
            if (!_pauseManager.isGameGoing || _playerPositionManager.isStunning)
            {
                return;
            }

            //メイン・サブの切り替え
            if (InputManager.mouseWheel > 0 && !isMainSelected)
            {
                isMainSelected = true;
                isSwitchingWeapon = true;
            }
            else if (InputManager.mouseWheel < 0 && isMainSelected)
            {
                isMainSelected = false;
                isSwitchingWeapon = true;

                //キャノンまたはレーザーを選択中の場合は使用をやめる
                //この部分をもっと綺麗に書く方法があれば教えて欲しい
                if (EquipmentData.equipmentData.selectedMainWeaponName == EquipmentData.equipmentType.MainWeapon__Cannon)
                {
                    _weaponInstances.cannon__Player.StopUsing();
                }
                else if (EquipmentData.equipmentData.selectedMainWeaponName == EquipmentData.equipmentType.MainWeapon__Laser)
                {
                    _weaponInstances.laser__Player.StopUsing();
                }
            }

            //左クリックを離すまで「切り替え中」にする
            if (isSwitchingWeapon && !InputManager.isMouseLeft)
            {
                isSwitchingWeapon = false;
            }
        }


        //攻撃関係の処理
        private void ProceedAttack()
        {
            //ゲームが進行中ではない　または　スタン中の時は攻撃できない
            if (!_pauseManager.isGameGoing || _playerPositionManager.isStunning) return;

            if (isMainSelected)
            {
                MainProceedAttack();
            }
            else
            {
                SubProceedAttack();
            }
        }
    }
}
