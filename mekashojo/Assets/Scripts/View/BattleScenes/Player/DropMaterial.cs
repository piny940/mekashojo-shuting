using UnityEngine;

namespace View
{
    public class DropMaterial : CollisionBase
    {
        [SerializeField, Header("タイプを選ぶ")] private Model.DropMaterialManager.materialType _type;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isDestroyed;

        private void Awake()
        {
            _id = ++Model.IDManager.lastMaterialID;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Model.DropMaterialManager dropMaterialManager
                = new Model.DropMaterialManager(
                    _type,
                    Controller.ModelClassController.playerStatusController,
                    Controller.ModelClassController.pauseController
                    );

            dropMaterialManager.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            dropMaterialManager.OnIsDestroyedChanged.AddListener((bool isDestroyed) =>
            {
                _isDestroyed = isDestroyed;
            });

            Controller.DropMaterialElements dropMaterialElements
                = new Controller.DropMaterialElements()
                {
                    dropMaterialManager = dropMaterialManager,
                    materialObject = this.gameObject,
                };

            Controller.PlayerClassController.dropMaterialTable.Add(_id, dropMaterialElements);

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
            if (_isDestroyed) Die();
        }

        private void Die()
        {
            Controller.PlayerClassController.dropMaterialTable.Remove(_id);
            Destroy(this.gameObject);
        }
    }
}
