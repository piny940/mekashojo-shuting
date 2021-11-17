using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class BeamMachineGun__Player : PlayerWeaponBase
    {
        private const int FIRE_PER_SECOND = 5;
        private int _beamMachineGunNumber = 0;
        private Vector3 _firingTarget;
        private int _count = 0;

        public UnityEvent OnBeamMachineGunNumberChanged = new UnityEvent();
        public UnityEvent<Vector3> OnFiringTargetChanged = new UnityEvent<Vector3>();

        public int beamMachineGunNumber
        {
            get { return _beamMachineGunNumber; }
            set
            {
                _beamMachineGunNumber = value;
                OnBeamMachineGunNumberChanged?.Invoke();
            }
        }

        protected override bool canAttack
        {
            get
            {
                return _count > 60 / FIRE_PER_SECOND
                        && InputManager.isMouseLeft
                        && playerStatusManager.mainEnergyAmount > 0;
            }
        }

        public Vector3 firingTarget
        {
            get { return _firingTarget; }
            set
            {
                _firingTarget = value;
                OnFiringTargetChanged?.Invoke(value);
            }
        }

        protected override void ProceedFirst() { }
        protected override void ProceedLast() { }

        public BeamMachineGun__Player(PlayerStatusManager playerStatusManager)
                : base(playerStatusManager) { }

        protected override void RunEveryFrame()
        {
            _count++;
        }

        protected override void Attack()
        {
            beamMachineGunNumber++;
            firingTarget = InputManager.mousePosition;

            _count = 0;

            playerStatusManager.mainEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedMainWeaponName]
                [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentData.selectedMainWeaponName]]
                [EquipmentData.equipmentParameter.Cost];
        }
    }
}
