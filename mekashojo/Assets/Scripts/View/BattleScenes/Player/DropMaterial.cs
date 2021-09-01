using UnityEngine;

namespace View
{
    public class DropMaterial : CollisionBase
    {
        [SerializeField, Header("タイプを選ぶ")] private Model.DropMaterialManager.materialType _type;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed;

        private void Awake()
        {
            _id = Controller.IDManager.GetMaterialID();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Model.DropMaterialManager dropMaterialManager
                = new Model.DropMaterialManager(
                    _type,
                    Controller.BattleScenesController.enemyManager,
                    Controller.BattleScenesController.playerStatusManager,
                    Controller.BattleScenesController.pauseManager
                    );

            dropMaterialManager.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            dropMaterialManager.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            Controller.DropMaterialElements dropMaterialElements
                = new Controller.DropMaterialElements()
                {
                    dropMaterialManager = dropMaterialManager,
                    materialObject = this.gameObject,
                };

            Controller.PlayerController.dropMaterialTable.Add(_id, dropMaterialElements);

            playOnEnter += (collision) =>
            {
                if (collision.tag == "BattleScenes/Player")
                {
                    dropMaterialManager.PickedUp();
                }
            };
        }

        // Update is called once per frame
        void Update()
        {
            if (_isBeingDestroyed) Die();
        }

        private void Die()
        {
            Controller.PlayerController.dropMaterialTable.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
