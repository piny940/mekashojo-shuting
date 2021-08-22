using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class BeamMachineGun__Player : PlayerWeaponBase
    {
        private const int FIRE_PER_SECOND = 5;
        private int _beamMachineGunNumber = 0;
        private int _count = 0;

        public UnityEvent OnBeamMachineGunNumberChanged = new UnityEvent();

        public int beamMachineGunNumber
        {
            get { return _beamMachineGunNumber; }
            set
            {
                _beamMachineGunNumber = value;
                OnBeamMachineGunNumberChanged?.Invoke();
            }
        }

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }
        protected override void RunEveryFrame() { }

        public BeamMachineGun__Player(PlayerStatusController playerStatusController) : base(playerStatusController) { }

        /// <summary>
        /// 攻撃できるかどうか
        /// </summary>
        /// <param name="playerStatusController"></param>
        /// <param name="weaponManager"></param>
        /// <returns></returns>
        protected override bool CanAttack()
        {
            _count++;
            return _count > 60 / FIRE_PER_SECOND && InputController.isMouseLeft && playerStatusController.mainEnergyAmount > 0;
        }

        /// <summary>
        /// 攻撃そのもの
        /// </summary>
        /// <param name="playerStatusController"></param>
        protected override void Attack()
        {
            beamMachineGunNumber++;

            _count = 0;

            playerStatusController.mainEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedMainWeaponName]
                [EquipmentData.equipmentData.equipmentLevel
                [EquipmentData.equipmentData.selectedMainWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
