using UnityEngine;

namespace View
{
    public class PlayerObject : CollisionBase
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.BattleScenesController.playerPositionManager.OnVelocityChanged.AddListener(
                (Vector3 velocity) => { _rigidbody2D.velocity = velocity; });
        }
    }
}
