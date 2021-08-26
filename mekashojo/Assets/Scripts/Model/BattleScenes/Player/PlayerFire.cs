using UnityEngine;

namespace Model
{
    public class PlayerFire : MovingObjectBase
    {
        private bool _willDisappearOnCollide;
        protected override movingObjectType objectType { get; set; }

        public PlayerFire(EnemyController enemyController, PauseController pauseController, bool willDisappearOnCollide) : base(enemyController, pauseController)
        {
            _willDisappearOnCollide = willDisappearOnCollide;
            objectType = movingObjectType.PlayerFire;
        }

        public void RunEveryFrame(Vector3 position)
        {
            StopOnPausing();
            DestroyLater(position);
        }

        public void DealDamage(EnemyDamageManager enemyDamageManager, float power)
        {
            //ダメージを与える
            enemyDamageManager.GetDamage(power);

            //敵と接触したら消滅するタイプの弾の時
            if (_willDisappearOnCollide)
            {
                isBeingDestroyed = true;
            }
        }
    }
}
