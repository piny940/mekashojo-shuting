using UnityEngine.Events;

namespace Model
{
    public class Balkan__Player : PlayerWeaponBase
    {
        private const int FIRE_PER_SECOND = 5;
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

        protected override bool canAttack
        {
            get
            {
                return _count > 60 / FIRE_PER_SECOND
                        && InputManager.isMouseLeft
                        && playerStatusManager.subEnergyAmount > 0;
            }
        }

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }

        public Balkan__Player(PlayerStatusManager playerStatusManager)
                : base(playerStatusManager) { }

        protected override void RunEveryFrame()
        {
            _count++;
        }

        protected override void Attack()
        {
            balkanNumber++;

            _count = 0;

            playerStatusManager.subEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedSubWeaponName]
                [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentData.selectedSubWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
