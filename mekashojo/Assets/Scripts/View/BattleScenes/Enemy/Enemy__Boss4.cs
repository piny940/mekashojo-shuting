using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Enemy__Boss4 : MonoBehaviour
    {
        [SerializeField, Header("Shieldを入れる")] private GameObject _shield;
        [SerializeField, Header("BossHPBarContentを入れる")] private Image _bossHPBarContent;
        private Rigidbody2D _rigidbody2D;
        private EnemyIDContainer _enemyIDContainer;
        private float _maxShieldScale;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyIDContainer = GetComponent<EnemyIDContainer>();

            _shield.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            _maxShieldScale = _shield.transform.localScale.x;

            Controller.EnemyController.EmergeEnemy__Boss4(this.gameObject);

            Model.Enemy__Boss4 enemy__Boss4
                = Controller.EnemyController.enemyTable
                    [Controller.EnemyController.enemyType__Rough.Boss4]
                    [Controller.EnemyController.bossID].enemy__Boss4;

            Model.EnemyDamageManager enemyDamageManager
                = Controller.EnemyController.damageManagerTable[Controller.EnemyController.bossID];

            _enemyIDContainer.id = Controller.EnemyController.bossID;

            // 速度の監視
            enemy__Boss4.OnVelocityChanged.AddListener((Vector3 velocity) =>
            {
                _rigidbody2D.velocity = velocity;
            });

            // シールドの使用時間の監視
            enemy__Boss4.OnShieldRestedTimeChanged.AddListener((float restedTime) =>
            {
                if (restedTime <= 0)
                {
                    _shield.SetActive(false);
                    return;
                }

                if (!_shield.activeSelf) _shield.SetActive(true);
                float scale = restedTime * _maxShieldScale / enemy__Boss4.shieldLimitTime;
                _shield.transform.localScale = new Vector3(scale, scale, 1);
            });

            // HPの監視
            enemyDamageManager.OnHPChanged.AddListener((float hp) =>
            {
                _bossHPBarContent.fillAmount = hp / Model.Enemy__Boss4.maxHP;
            });
        }
    }
}
