using UnityEngine;

namespace View
{
    // Prefabからゲームオブジェクトを生成する際、個別でパスを入力しているとタイポの
    // 可能性が高まるため、このクラスでオブジェクトを生成する処理を担う
    public class PrefabManager : MonoBehaviour
    {
        // 敵を生成する際に用いる
        public static GameObject ProduceEnemy(Controller.NormalEnemyData.normalEnemyType type, Vector3 position)
        {
            return Instantiate((GameObject)Resources.Load(
                $"Prefab/BattleScenes/Enemy/Enemy__{type}"),
                position,
                Quaternion.identity
                );
        }

        // DropItemを生成する際に用いる
        public static GameObject ProduceDropItem(Model.DropMaterialManager.materialType type, Vector3 position)
        {
            return Instantiate((GameObject)Resources.Load(
                $"Prefab/BattleScenes/DropMaterial/{type}"),
                position,
                Quaternion.identity);
        }

        // 敵の爆発エフェクトを生成する際に用いる
        public static GameObject ProduceEnemyExplodeEffect(Controller.NormalEnemyData.normalEnemyType type, Vector3 position)
        {
            return Instantiate((GameObject)Resources.Load(
                $"Prefab/BattleScenes/Enemy/ExplodeEffect__Enemy__{type}"),
                position,
                Quaternion.identity
                );
        }

        // その他BattlεSceneで用いるオブジェクトを生成する際に用いる
        public static GameObject ProduceObject(string path, Vector3 position)
        {
            return Instantiate((GameObject)Resources.Load(
                $"Prefab/BattleScenes/{path}"),
                position,
                Quaternion.identity
                );
        }
    }
}
