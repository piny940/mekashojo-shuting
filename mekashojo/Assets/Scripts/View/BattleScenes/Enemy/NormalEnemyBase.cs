using System.Collections.ObjectModel;
using UnityEngine;

namespace View
{
    public class NormalEnemyBase : FiringBulletBase
    {
        [SerializeField, Header("NormalEnemyDataを入れる")] protected Model.NormalEnemyData _normalEnemyData;
        protected Rigidbody2D rigidbody2D;
        protected int id;
        protected bool isDead;

        private EnemyIDContainer _idContainer;
        private ObservableCollection<int> _lastmaterialNumbers;

        protected void CallAtAwake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            _idContainer = GetComponent<EnemyIDContainer>();
        }

        protected Model.EnemyDamageManager Initialize()
        {
            //IDの取得
            id = _idContainer.id;

            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(Controller.ModelClassController.enemyController, _normalEnemyData);

            _lastmaterialNumbers = new ObservableCollection<int>(enemyDamageManager.materialNumbers);

            //ドロップアイテムの生成
            enemyDamageManager.materialNumbers.CollectionChanged += (sender, e) =>
            {
                OnMaterialNumbersChanged(enemyDamageManager);
            };

            return enemyDamageManager;
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
                    Instantiate(
                        (GameObject)Resources.Load("Prefab/BattleScenes/DropMaterial/" + type.ToString()),
                        transform.position,
                        Quaternion.identity);

                    _lastmaterialNumbers[(int)type] = enemyDamageManager.materialNumbers[(int)type];

                    break;
                }
            }
        }
    }
}
