using UnityEngine;

namespace View
{
    public class BombFire__Player : CollisionBase
    {
        void Start()
        {
            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Enemy")
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
                = Controller.EnemyClassController.damageManagerTable[enemyIDContainer.id];

            Controller.PlayerClassController.bombFire__Player.Attack(enemyDamageManager);
        }
    }
}

