using UnityEngine;

namespace View
{
    // BeamMachineGun, Balkan, Missileにはこのクラスをつける
    public class PlayerBullet : CollisionBase
    {
        [SerializeField, Header("武器のタイプを選ぶ")] private Model.EquipmentData.equipmentType _type;
        private int _id;
        private float _power;
        private bool _isDestroyed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _id = ++Model.IDManager.lastPlayerBulletID;
            _power = Model.EquipmentData.equipmentData.equipmentStatus[_type]
                    [Model.EquipmentData.equipmentData.equipmentLevel[_type]]
                    [Model.EquipmentData.equipmentParameter.Power];
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Model.PlayerFire playerFire
                = new Model.PlayerFire(Controller.BattleScenesClassController.pauseController, true);

            playerFire.OnIsDestroyedChanged.AddListener((bool isDestroyed) =>
            {
                _isDestroyed = isDestroyed;
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

            Controller.PlayerClassController.playerBulletTable.Add(_id, playerBulletElements);

            playWhileIn += (collision) =>
            {
                if (collision.tag == "BattleScenes/Enemy")
                {
                    DoDamage(collision);
                }
            };
        }

        private void Update()
        {
            if (_isDestroyed) Die();
        }

        private void DoDamage(Collider2D collision)
        {
            EnemyIDContainer enemyIDContainer = collision.GetComponent<EnemyIDContainer>();

            if (enemyIDContainer == null) throw new System.Exception();

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyClassController.damageManagerTable[enemyIDContainer.id];

            Controller.PlayerClassController.playerBulletTable[_id]
                .playerFire.DoDamage(enemyDamageManager, _power);
        }

        private void Die()
        {
            Controller.PlayerClassController.playerBulletTable.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
