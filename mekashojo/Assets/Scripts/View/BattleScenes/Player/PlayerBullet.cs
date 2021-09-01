using UnityEngine;

namespace View
{
    // BeamMachineGun, Balkan, Missileにはこのクラスをつける
    public class PlayerBullet : CollisionBase
    {
        [SerializeField, Header("武器のタイプを選ぶ")] private Model.EquipmentData.equipmentType _type;
        private int _id;
        private bool _isBeingDestroyed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _id = Controller.IDManager.GetPlayerBulletID();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Model.PlayerFire playerFire
                = new Model.PlayerFire(
                    Controller.BattleScenesController.enemyManager,
                    Controller.BattleScenesController.pauseManager,
                    _type
                    );

            playerFire.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            playerFire.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            Controller.PlayerBulletElements playerBulletElements = new Controller.PlayerBulletElements()
            {
                playerFire = playerFire,
                bulletObject = this.gameObject,
            };

            Controller.PlayerController.playerBulletTable.Add(_id, playerBulletElements);

            playWhileIn += (collision) =>
            {
                if (collision.tag == "BattleScenes/Enemy")
                {
                    DealDamage(collision);
                }
            };
        }

        private void Update()
        {
            if (_isBeingDestroyed) Die();
        }

        private void DealDamage(Collider2D collision)
        {
            EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

            if (enemyIDContainer == null) throw new System.Exception();

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[enemyIDContainer.id];

            Controller.PlayerController.playerBulletTable[_id]
                .playerFire.DealDamage(enemyDamageManager);
        }

        private void Die()
        {
            Controller.PlayerController.playerBulletTable.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
