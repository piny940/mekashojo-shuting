using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    //キャノンとレーザーのコードが全く一緒なので基底クラスにまとめて、
    //Cannon__PlayerとLaser__Playerにこのクラスを継承させる
    public class CannonAndLase__PlayerBase : PlayerWeaponBase
    {
        bool _isEnergyScarce;
        private Vector3 _firingTarget = Vector3.zero;
        private bool _isFireVisible = false;

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

        public CannonAndLase__PlayerBase(PlayerStatusController playerStatusController) : base(playerStatusController) { }

        /// <summary>
        /// 使用をやめる
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
            if (playerStatusController.mainEnergyAmount <= 0)
                _isEnergyScarce = true;
            else if (!InputController.isMouseLeft && _isEnergyScarce)
                _isEnergyScarce = false;
        }

        /// <summary>
        /// 攻撃そのもの
        /// </summary>
        protected override void Attack()
        {
            //ミサイルの向きを変える
            firingTarget = InputController.mousePosition;

            //エネルギーを減らす
            playerStatusController.mainEnergyAmount
                -= EquipmentData.equipmentData.equipmentStatus
                [EquipmentData.equipmentData.selectedMainWeaponName]
                [EquipmentData.equipmentData.equipmentLevel
                [EquipmentData.equipmentData.selectedMainWeaponName]]
                [EquipmentData.equipmentParameter.Cost] * Time.deltaTime;
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

        /// <summary>
        /// 攻撃し続けることができるか
        /// </summary>
        /// <returns></returns>
        protected override bool CanAttack()
            => InputController.isMouseLeft
                && playerStatusController.mainEnergyAmount > 0
                && !_isEnergyScarce;
    }
}
