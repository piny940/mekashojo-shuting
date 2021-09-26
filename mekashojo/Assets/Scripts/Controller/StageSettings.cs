using UnityEngine;

namespace Controller
{
    [CreateAssetMenu(menuName = "ScriptableObject/StageSettings")]
    public class StageSettings : ScriptableObject
    {
        [Header("ボスの定位置")] public Vector3 bossPosition;
        [Header("初めにいる敵の数")] public int firstEnemyAmount;
        [Header("敵の数の上限")] public int maxEnemyAmount;
        [Header("敵の生成確率曲線")] public AnimationCurve enemyProduceProbabilityCurve;

        [Header("各敵の生成比")]
        public int spreadBulletEnemyProduceProbabilityRatio;
        public int singleBulletEnemyProduceProbabilityRatio;
        public int stunBulletEnemyProduceProbabilityRatio;
        public int fastBulletEnemyProduceProbabilityRatio;
        public int slowBulletEnemyProduceProbabilityRatio;
        public int missileEnemyProduceProbabilityRatio;
        public int repeatedEnemyProduceProbabilityRatio;
        public int wideBeamEnemyProduceProbabilityRatio;
        public int guidedBulletEnemyProduceProbabilityRatio;
        public int wideSpreadBulletEnemyProduceProbabilityRatio;
        public int selfDestructEnemyProduceProbabilityRatio;
    }
}
