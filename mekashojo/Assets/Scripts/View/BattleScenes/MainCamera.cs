using UnityEngine;

namespace View
{
    public class MainCamera : MonoBehaviour
    {
        private const float APPROACHING_SPEED = 3;
        private Vector3 _fromPlayerToCamera;
        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("BattleScenes/Player");
        }

        private void Start()
        {
            _fromPlayerToCamera = transform.position - _player.transform.position;
        }
        void Update()
        {
            float distance
                = Vector3.Magnitude(transform.position - (_player.transform.position + _fromPlayerToCamera)); //定位置との距離

            transform.position
                = Vector3.MoveTowards(
                    transform.position,
                    _player.transform.position + _fromPlayerToCamera,
                    APPROACHING_SPEED * Time.deltaTime);
        }
    }
}
