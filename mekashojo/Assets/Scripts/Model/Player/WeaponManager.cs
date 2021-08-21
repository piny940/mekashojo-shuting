using System;
using UnityEngine.Events;

namespace Model
{
    //武器のクラスのインスタンスを一つの構造体としてまとめておく
    public struct WeaponInstances
    {
        public Cannon__Player cannon__Player;
        public Missile__Player missile__Player;
    }

    public class WeaponManager
    {
        public Action MainProceedAttack;

        public Action SubProceedAttack;

        public UnityEvent<bool> OnWeaponSwitched = new UnityEvent<bool>();

        private bool _isMainSelected = false;

        public bool isSwitchingWeapon;

        public bool isMainSelected
        {
            get { return _isMainSelected; }
            set
            {
                _isMainSelected = value;
                OnWeaponSwitched?.Invoke(_isMainSelected);
            }
        }

        private PauseController _pauseController;
        private PlayerPositionController _playerPositionController;
        private WeaponInstances _weaponInstances;

        public WeaponManager(PauseController pauseController, PlayerPositionController playerPositionController, WeaponInstances weaponInstances)
        {
            _pauseController = pauseController;
            _playerPositionController = playerPositionController;
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

                default:
                    break;
            }

            switch (EquipmentData.equipmentData.selectedSubWeaponName)
            {
                case EquipmentData.equipmentType.SubWeapon__Missile:
                    SubProceedAttack = _weaponInstances.missile__Player.Execute;
                    break;

                default:
                    break;
            }
        }


        public void SwitchWeapon()
        {
            //ゲームが進行中ではない　または　スタン中の時は武器の切り替えはできない
            if (!_pauseController.isGameGoing || _playerPositionController.isStunning)
            {
                return;
            }

            //メイン・サブの切り替え
            if (InputController.mouseWheel > 0 && !isMainSelected)
            {
                isMainSelected = true;
                isSwitchingWeapon = true;
            }
            else if (InputController.mouseWheel < 0 && isMainSelected)
            {
                isMainSelected = false;
                isSwitchingWeapon = true;

                //キャノンを選択中の場合は使用をやめる
                //この部分をもっと綺麗に書く方法があれば教えて欲しい
                if (EquipmentData.equipmentData.selectedMainWeaponName == EquipmentData.equipmentType.MainWeapon__Cannon)
                {
                    _weaponInstances.cannon__Player.StopUsing();
                }
            }

            //左クリックを離すまで「切り替え中」にする
            if (isSwitchingWeapon && !InputController.isMouseLeft)
            {
                isSwitchingWeapon = false;
            }
        }


        //攻撃関係の処理
        public void ProceedAttack()
        {
            //ゲームが進行中ではない　または　スタン中の時は攻撃できない
            if (!_pauseController.isGameGoing || _playerPositionController.isStunning)
            {
                return;
            }

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
