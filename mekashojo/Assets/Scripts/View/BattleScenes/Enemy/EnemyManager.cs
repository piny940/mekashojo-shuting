using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

namespace View
{
    public class EnemyManager : MonoBehaviour
    {
        private const float SCREEN_FRAME_WIDTH = 1.5f;
        private const float UI_WIDTH = 1.5f;

        [SerializeField, Header("ステージ名を選ぶ")] private Model.ProgressData.stageName _stageName;

        private ObservableCollection<int> _lastEnemyNumbers;

        // Start is called before the first frame update
        void Start()
        {
            //初期化
            _lastEnemyNumbers = new ObservableCollection<int>(Controller.BattleScenesController.enemyManager.enemyNumbers);

            Controller.BattleScenesController.enemyManager.enemyNumbers.CollectionChanged +=
                (object sender, NotifyCollectionChangedEventArgs e) =>
                {
                    foreach (Controller.NormalEnemyData.normalEnemyType type in System.Enum.GetValues(typeof(Controller.NormalEnemyData.normalEnemyType)))
                    {
                        if (_lastEnemyNumbers[(int)type] != Controller.BattleScenesController.enemyManager.enemyNumbers[(int)type])
                        {
                            ProduceEnemy(type);
                            _lastEnemyNumbers[(int)type] = Controller.BattleScenesController.enemyManager.enemyNumbers[(int)type];
                            break;
                        }
                    }
                };
        }

        private void ProduceEnemy(Controller.NormalEnemyData.normalEnemyType type)
        {
            //画面左下と右上の座標の取得
            Vector3 cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            //敵は画面の右半分に生成する仕様にしている
            //上下左右に「敵が出現しない空間」を設けておかないと敵がスクリーンの端っこに生成されてしまうため、SCREEN_FRAME_WIDTHによってスクリーン端には敵を生成しないようにした
            Vector3 producePosition = new Vector3(
                Random.Range((cornerPosition__LeftBottom.x + cornerPosition__RightTop.x) / 2, cornerPosition__RightTop.x - SCREEN_FRAME_WIDTH),
                Random.Range(cornerPosition__LeftBottom.y + SCREEN_FRAME_WIDTH, cornerPosition__RightTop.y - SCREEN_FRAME_WIDTH - UI_WIDTH),
                Model.EnemyManager.enemyPosition__z);

            //指定された敵の生成
            _ = PrefabManager.ProduceEnemy(type, _stageName, producePosition);
        }
    }
}
