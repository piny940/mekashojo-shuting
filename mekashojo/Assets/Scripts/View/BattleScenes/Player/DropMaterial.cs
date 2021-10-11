using UnityEngine;

namespace View
{
    public class DropMaterial : CollisionBase
    {
        [SerializeField, Header("タイプを選ぶ")] private Model.DropMaterialManager.materialType _type;
        [SerializeField, Header("拾った時になる音を入れる")] private AudioClip _pickUpSound;
        private int _id;
        private Rigidbody2D _rigidbody2D;
        private bool _isBeingDestroyed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _id = Controller.PlayerController.EmergeDropMaterial(_type, this.gameObject);

            Model.DropMaterialManager dropMaterialManager
                = Controller.PlayerController.dropMaterialTable[_id].dropMaterialManager;

            dropMaterialManager.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            dropMaterialManager.OnIsBeingDestroyedChanged.AddListener((bool isBeingDestroyed) =>
            {
                _isBeingDestroyed = isBeingDestroyed;
            });

            playOnEnter += (collision) =>
            {
                if (collision.tag == TagManager.TagNames.BattleScenes__Player.ToString())
                {
                    SEPlayer.sePlayer.PlayOneShot(_pickUpSound);
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
