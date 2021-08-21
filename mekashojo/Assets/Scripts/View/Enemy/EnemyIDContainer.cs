using UnityEngine;

namespace View
{
    public class EnemyIDContainer : MonoBehaviour
    {
        public int id { get; private set; }

        private void Awake()
        {
            id = ++Model.EnemyController.lastID;
        }
    }
}
