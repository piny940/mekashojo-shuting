using UnityEngine;

namespace View
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField, Header("定位置との距離に応じた、定位置に近づく速さ")] private AnimationCurve _speed;
        private const float LEFT_LIMIT = -11.2f;
        private const float RIGHT_LIMIT = 417;
        private const float APPROACHING_SPEED = 3;
        private Vector3 _fromPlayerToCamera;
        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag(TagManager.TagNames.BattleScenes__Player.ToString());
        }

        private void Start()
        {
            _fromPlayerToCamera = transform.position - _player.transform.position;
        }
        void Update()
        {
            float distance
                = Vector3.Magnitude(transform.position - (_player.transform.position + _fromPlayerToCamera)); //定位置との距離

            Vector3 newPosition = new Vector3();

            newPosition.x
                = Vector3.MoveTowards(
                    transform.position,
                    _player.transform.position + _fromPlayerToCamera,
                    APPROACHING_SPEED * _speed.Evaluate(distance) * Time.deltaTime
                    ).x;

            if (newPosition.x < LEFT_LIMIT) newPosition.x = LEFT_LIMIT;
            if (newPosition.x > RIGHT_LIMIT) newPosition.x = RIGHT_LIMIT;

            newPosition.y = transform.position.y;
            newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
    }
}
