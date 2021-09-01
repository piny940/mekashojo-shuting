using UnityEngine.Events;

namespace Model
{
    public class Missile__Player : PlayerWeaponBase
    {
        private bool _hasAttacked = false;
        private int _missileNumber = 0;

        public UnityEvent OnMissileNumberChanged = new UnityEvent();

        public int missileNumber
        {
            get { return _missileNumber; }
            set
            {
                _missileNumber = value;
                OnMissileNumberChanged?.Invoke();
            }
        }

        // 攻撃できるかどうか
        protected override bool canAttack
        {
            get
            {
                return !_hasAttacked && InputManager.isMouseLeft
                        && playerStatusManager.subEnergyAmount > 0
                        && !Controller.BattleScenesController.weaponManager.isSwitchingWeapon;
            }
        }

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }

        public Missile__Player(PlayerStatusManager playerStatusManager)
                : base(playerStatusManager) { }

        protected override void RunEveryFrame()
        {
            //左クリックを離した瞬間の処理
            if (_hasAttacked && !InputManager.isMouseLeft)
            {
                _hasAttacked = false;
            }
        }

        protected override void Attack()
        {
            _hasAttacked = true;

            missileNumber++;

            playerStatusManager.subEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedSubWeaponName]
                [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentData.selectedSubWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
