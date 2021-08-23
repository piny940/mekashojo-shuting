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

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }
        protected override void RunEveryFrame() { }

        public Missile__Player(PlayerStatusController playerStatusController) : base(playerStatusController) { }

        /// <summary>
        /// 攻撃できるかどうか
        /// </summary>
        /// <param name="playerStatusController"></param>
        /// <param name="weaponManager"></param>
        /// <returns></returns>
        protected override bool CanAttack()
        {
            //左クリックを離した瞬間の処理
            if (_hasAttacked && !InputController.isMouseLeft)
            {
                _hasAttacked = false;
            }

            return
                !_hasAttacked && InputController.isMouseLeft
                && playerStatusController.subEnergyAmount > 0
                && !Controller.BattleScenesClassController.weaponManager.isSwitchingWeapon;
        }

        /// <summary>
        /// 攻撃そのもの
        /// </summary>
        /// <param name="playerStatusController"></param>
        protected override void Attack()
        {
            _hasAttacked = true;

            missileNumber++;

            playerStatusController.subEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedSubWeaponName]
                [EquipmentData.equipmentData.equipmentLevel
                [EquipmentData.equipmentData.selectedSubWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
