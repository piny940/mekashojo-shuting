using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Laser__Player : PlayerWeaponBase
    {
        private bool _isEnergyScarce;
        private bool _isUsingLaser = false;
        private Vector3 _firingTarget = Vector3.zero;

        public UnityEvent<Vector3> OnFiringTargetChanged = new UnityEvent<Vector3>();
        public UnityEvent<bool> OnIsUsingLaserChanged = new UnityEvent<bool>();

        public Vector3 firingTarget
        {
            get { return _firingTarget; }
            set
            {
                _firingTarget = value;
                OnFiringTargetChanged?.Invoke(_firingTarget);
            }
        }

        public bool isUsingLaser
        {
            get { return _isUsingLaser; }
            set
            {
                _isUsingLaser = value;
                OnIsUsingLaserChanged?.Invoke(_isUsingLaser);
            }
        }

        protected override bool canAttack
        {
            get
            {
                return InputManager.isMouseLeft
                        && playerStatusManager.mainEnergyAmount > 0
                        && !_isEnergyScarce;
            }
        }

        public Laser__Player(PlayerStatusManager playerStatusManager)
                : base(playerStatusManager) { }

        /// <summary>
        /// WeaponManagerで武器を切り替える際に、「使用をやめる」処理を行わないと
        /// キャノン/レーザーが使われたままになってしまうため、
        /// 武器をメインからサブに切り替えるタイミングでこのメソッドを呼ぶ
        /// </summary>
        public void StopUsing()
        {
            ProceedLast();
            lastCanAttack = false;
        }

        //エネルギー不足の時にビームがチカチカするのを防ぐために,
        //エネルギーが一度ゼロになったら左クリックを離すまでは
        //_isEnergyScarceをtrueにしてビームが出ないようにする
        protected override void RunEveryFrame()
        {
            if (playerStatusManager.mainEnergyAmount <= 0)
                _isEnergyScarce = true;
            else if (!InputManager.isMouseLeft && _isEnergyScarce)
                _isEnergyScarce = false;
        }

        protected override void Attack()
        {
            //ミサイルの向きを変える
            firingTarget = InputManager.mousePosition;

            //エネルギーを減らす
            playerStatusManager.mainEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedMainWeaponName]
                [EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentData.selectedMainWeaponName]]
                [EquipmentData.equipmentParameter.Cost]
                    * Time.deltaTime;
        }

        /// <summary>
        /// 攻撃のはじめにする処理
        /// </summary>
        protected override void ProceedFirst()
        {
            isUsingLaser = true;
        }

        /// <summary>
        /// 攻撃の最後にする処理
        /// </summary>
        protected override void ProceedLast()
        {
            isUsingLaser = false;
        }
    }
}
