using UnityEngine;

namespace View
{
    public class EnemyFire__Bullet : CollisionBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] Model.NormalEnemyData _normalEnemyData;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isDestroyed;

        private void Awake()
        {
            _id = ++Model.IDManager.lastEnemyBulletID;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Model.EnemyFire enemyFire = new Model.EnemyFire(
                _normalEnemyData,
                Controller.ModelClassController.playerStatusController,
                Controller.ModelClassController.playerPositionController,
                Controller.ModelClassController.pauseController
                );

            enemyFire.OnIsDestroyedChanged.AddListener((bool isDestroyed) =>
            {
                _isDestroyed = isDestroyed;
            });

            enemyFire.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            playOnEnter += (Collider2D collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    enemyFire.Attack();
                }
            };

            Controller.EnemyFireElements enemyFireElements
                = new Controller.EnemyFireElements()
                {
                    enemyFire = enemyFire,
                    enemyFireObject = this.gameObject,
                };

            Controller.EnemyClassController.fireTable__Bullet.Add(_id, enemyFireElements);
        }

        private void Update()
        {
            if (_isDestroyed) Die();
        }

        private void Die()
        {
            Controller.EnemyClassController.fireTable__Bullet.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
