using System.Collections.ObjectModel;
using UnityEngine;

namespace View
{
    public class NormalEnemyBase : FiringBulletBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] protected Controller.NormalEnemyData normalEnemyData;
        protected Rigidbody2D rigidbody2D;
        protected bool isBeingDestroyed;
        private EnemyIDContainer _idContainer;
        private ObservableCollection<int> _lastmaterialNumbers;

        protected void CallAtAwake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            _idContainer = GetComponent<EnemyIDContainer>();
        }

        // このメソッドは子クラスでIDを取得した後に呼ばれることを保証するために
        // あえてIDを引数で渡してる(蛇足？)
        protected void Initialize(int id)
        {
            // IDContainerにIDを渡す
            _idContainer.id = id;

            Model.EnemyDamageManager enemyDamageManager = Controller.EnemyController.damageManagerTable[id];

            // lastmaterialNumbersの初期化
            _lastmaterialNumbers = new ObservableCollection<int>(enemyDamageManager.materialNumbers);

            //ドロップアイテムの生成
            enemyDamageManager.materialNumbers.CollectionChanged += (sender, e) =>
            {
                OnMaterialNumbersChanged(enemyDamageManager);
            };

            // 消滅の監視
            enemyDamageManager.OnIsDyingChanged.AddListener((bool isDying) =>
            {
                isBeingDestroyed = isDying;
            });
        }

        /// <summary>
        /// materialNumbersに変更が加わった時の処理
        /// ドロップアイテムを落とす
        /// </summary>
        private void OnMaterialNumbersChanged(Model.EnemyDamageManager enemyDamageManager)
        {
            foreach (Model.DropMaterialManager.materialType type in System.Enum.GetValues(typeof(Model.DropMaterialManager.materialType)))
            {
                if (_lastmaterialNumbers[(int)type] != enemyDamageManager.materialNumbers[(int)type])
                {
                    PrefabManager.ProduceDropItem(type, transform.position);

                    _lastmaterialNumbers[(int)type] = enemyDamageManager.materialNumbers[(int)type];

                    break;
                }
            }
        }
    }
}
