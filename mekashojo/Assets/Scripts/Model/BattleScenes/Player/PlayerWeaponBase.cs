namespace Model
{
    public abstract class PlayerWeaponBase
    {
        protected bool lastCanAttack = false;
        protected PlayerStatusManager playerStatusManager;

        protected abstract void ProceedFirst();
        protected abstract void Attack();
        protected abstract void ProceedLast();
        protected abstract bool canAttack { get; }
        protected abstract void RunEveryFrame();

        public PlayerWeaponBase(PlayerStatusManager playerStatusManager)
        {
            this.playerStatusManager = playerStatusManager;
        }

        public void Execute()
        {
            //毎フレームする処理
            RunEveryFrame();

            bool canAttack = this.canAttack;

            //攻撃のはじめにする処理
            if (!lastCanAttack && canAttack)
            {
                ProceedFirst();
            }

            //攻撃の終わりにする処理
            if (lastCanAttack && !canAttack)
            {
                ProceedLast();
            }

            lastCanAttack = canAttack;

            //攻撃そのもの
            if (lastCanAttack)
            {
                Attack();
            }
        }
    }
}
