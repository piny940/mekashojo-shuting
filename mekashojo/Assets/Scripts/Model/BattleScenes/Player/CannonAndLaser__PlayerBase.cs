using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    //キャノンとレーザーのコードが全く一緒なので基底クラスにまとめて、
    //Cannon__PlayerとLaser__Playerにこのクラスを継承させる
    public class CannonAndLase__PlayerBase : PlayerWeaponBase
    {
        private bool _isEnergyScarce;
        private bool _isFireVisible = false;
        private Vector3 _firingTarget = Vector3.zero;

        public UnityEvent<Vector3> OnFiringTargetChanged = new UnityEvent<Vector3>();
        public UnityEvent<bool> OnFireVisibilityChanged = new UnityEvent<bool>();

        public Vector3 firingTarget
        {
            get { return _firingTarget; }
            set
            {
                _firingTarget = value;
                OnFiringTargetChanged?.Invoke(_firingTarget);
            }
        }

        public bool isFireVisible
        {
            get { return _isFireVisible; }
            set
            {
                _isFireVisible = value;
                OnFireVisibilityChanged?.Invoke(_isFireVisible);
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

        public CannonAndLase__PlayerBase(PlayerStatusManager playerStatusManager)
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
            isFireVisible = true;
        }

        /// <summary>
        /// 攻撃の最後にする処理
        /// </summary>
        protected override void ProceedLast()
        {
            isFireVisible = false;
        }
    }
}
