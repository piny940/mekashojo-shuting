namespace Model
{
    public abstract class PlayerWeaponBase
    {
        protected bool lastCanAttack = false;
        protected PlayerStatusController playerStatusController;

        protected abstract void ProceedFirst();
        protected abstract void Attack();
        protected abstract void ProceedLast();
        protected abstract bool CanAttack();
        protected abstract void RunEveryFrame();

        public PlayerWeaponBase(PlayerStatusController playerStatusController)
        {
            this.playerStatusController = playerStatusController;
        }

        public void Execute()
        {
            //毎フレームする処理
            RunEveryFrame();

            //攻撃のはじめにする処理
            if (!lastCanAttack && CanAttack())
            {
                ProceedFirst();
            }

            //攻撃の終わりにする処理
            if (lastCanAttack && !CanAttack())
            {
                ProceedLast();
            }

            lastCanAttack = CanAttack();

            //攻撃そのもの
            if (lastCanAttack)
            {
                Attack();
            }
        }
    }
}
