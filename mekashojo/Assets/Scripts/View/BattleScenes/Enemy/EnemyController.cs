using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

namespace View
{
    // このクラス、横長なコードが多いんやけどどこで改行するのが正しいんかわからん
    public class EnemyController : MonoBehaviour
    {
        private ObservableCollection<int> _lastEnemyNumbers;

        private const float SCREEN_FRAME_WIDTH = 1;
        private const float UI_WIDTH = 1.5f;

        // Start is called before the first frame update
        void Start()
        {
            //初期化
            _lastEnemyNumbers = new ObservableCollection<int>(Controller.ModelClassController.enemyController.enemyNumbers);

            Controller.ModelClassController.enemyController.enemyNumbers.CollectionChanged +=
                (object sender, NotifyCollectionChangedEventArgs e) =>
                {
                    foreach (Model.NormalEnemyData.normalEnemyType type in System.Enum.GetValues(typeof(Model.NormalEnemyData.normalEnemyType)))
                    {
                        // この部分もうちょっと綺麗にしたい。
                        if (_lastEnemyNumbers[(int)type] != Controller.ModelClassController.enemyController.enemyNumbers[(int)type])
                        {
                            ProduceEnemy(type);
                            _lastEnemyNumbers[(int)type] = Controller.ModelClassController.enemyController.enemyNumbers[(int)type];
                            break;
                        }
                    }
                };
        }

        private void ProduceEnemy(Model.NormalEnemyData.normalEnemyType type)
        {
            //画面左下と右上の座標の取得
            Vector3 cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            //敵は画面の右半分に生成する仕様にしている
            //上下左右に「敵が出現しない空間」を設けておかないと敵がスクリーンの端っこに生成されてしまうため、SCREEN_FRAME_WIDTHによってスクリーン端には敵を生成しないようにした
            Vector3 producePosition = new Vector3(
                Random.Range((cornerPosition__LeftBottom.x + cornerPosition__RightTop.x) / 2, cornerPosition__RightTop.x - SCREEN_FRAME_WIDTH),
                Random.Range(cornerPosition__LeftBottom.y + SCREEN_FRAME_WIDTH, cornerPosition__RightTop.y - SCREEN_FRAME_WIDTH - UI_WIDTH),
                Model.EnemyController.enemyPosition__z);

            //指定された敵の生成
            Instantiate((GameObject)Resources.Load(
                "Prefab/BattleScenes/Enemy/Enemy__" + type.ToString()),
                producePosition,
                Quaternion.identity
                );
        }
    }
}
