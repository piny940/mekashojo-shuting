namespace Model
{
    public class PlayerFire : MovingObjectBase
    {
        private bool _willDisappearOnCollide;

        public PlayerFire(PauseController pauseController, bool willDisappearOnCollide) : base(pauseController)
        {
            _willDisappearOnCollide = willDisappearOnCollide;
        }

        public void DoDamage(EnemyDamageManager enemyDamageManager, float power)
        {
            //ダメージを与える
            enemyDamageManager.GetDamage(power);

            //敵と接触したら消滅するタイプの弾の時
            if (_willDisappearOnCollide)
            {
                isDestroyed = true;
            }
        }
    }
}
