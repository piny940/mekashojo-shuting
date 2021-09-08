using UnityEngine;

namespace View
{
    public class BombFire__Player : CollisionBase
    {
        void Start()
        {
            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Enemy.ToString())
                {
                    DealDamage(collision);
                }
            };
        }

        private void DealDamage(Collider2D collision)
        {
            EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

            if (enemyIDContainer == null) throw new System.Exception();

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[enemyIDContainer.id];

            Controller.PlayerController.bombFire__Player.Attack(enemyDamageManager);
        }
    }
}

