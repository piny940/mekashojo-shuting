using UnityEngine.Events;

namespace Model
{
    public class Balkan__Player : PlayerWeaponBase
    {
        private const int FIRE_PER_SECOND = 15;
        private int _balkanNumber = 0;
        private int _count = 0;

        public UnityEvent OnBalkanNumberChanged = new UnityEvent();

        public int balkanNumber
        {
            get { return _balkanNumber; }
            set
            {
                _balkanNumber = value;
                OnBalkanNumberChanged?.Invoke();
            }
        }

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }
        protected override void RunEveryFrame() { }

        public Balkan__Player(PlayerStatusController playerStatusController) : base(playerStatusController) { }

        /// <summary>
        /// 攻撃できるかどうか
        /// </summary>
        /// <param name="playerStatusController"></param>
        /// <param name="weaponManager"></param>
        /// <returns></returns>
        protected override bool CanAttack()
        {
            _count++;
            return _count > 60 / FIRE_PER_SECOND && InputController.isMouseLeft && playerStatusController.subEnergyAmount > 0;
        }

        /// <summary>
        /// 攻撃そのもの
        /// </summary>
        /// <param name="playerStatusController"></param>
        protected override void Attack()
        {
            balkanNumber++;

            _count = 0;

            playerStatusController.subEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedSubWeaponName]
                [EquipmentData.equipmentData.equipmentLevel
                [EquipmentData.equipmentData.selectedSubWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
