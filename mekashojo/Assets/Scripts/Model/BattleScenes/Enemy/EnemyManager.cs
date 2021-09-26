using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Model
{
    public class EnemyManager
    {
        private Dictionary<Controller.NormalEnemyData.normalEnemyType, float> _produceProbabilityRatios; //各敵を生成する確率の生成比

        private StageStatusManager _stageStatusManager;

        public int totalEnemyAmount { get; set; }   //今いる敵の数
        public static readonly float enemyPosition__z = 10;

        //生成した敵の数(今ステージ上にいる敵の数とは無関係)
        //この配列の変化をViewが検知して敵を生成する
        //逆に言うと、enemyNumbersはViewに変更を通知するためだけの値であって
        //何か意味を持った値ではない
        public ObservableCollection<int> enemyNumbers { get; private set; }
            = new ObservableCollection<int>(new int[System.Enum.GetNames(typeof(Controller.NormalEnemyData.normalEnemyType)).Length]);

        public EnemyManager(StageStatusManager stageStatusManager, Controller.StageSettings stageSettings)
        {
            _stageStatusManager = stageStatusManager;

            _produceProbabilityRatios = new Dictionary<Controller.NormalEnemyData.normalEnemyType, float>()
            {
                { Controller.NormalEnemyData.normalEnemyType.SpreadBullet, stageSettings.spreadBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SingleBullet, stageSettings.singleBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.StunBullet, stageSettings.stunBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.FastBullet, stageSettings.fastBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SlowBullet, stageSettings.slowBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.Missile, stageSettings.missileEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.RepeatedFire, stageSettings.repeatedEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.WideBeam, stageSettings.wideBeamEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.GuidedBullet, stageSettings.guidedBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.WideSpreadBullet, stageSettings.wideSpreadBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SelfDestruct, stageSettings.selfDestructEnemyProduceProbabilityRatio },
            };

            //はじめにいる敵の数
            totalEnemyAmount = stageSettings.firstEnemyAmount;
        }

        public void RunEveryFrame(Controller.StageSettings stageSettings)
        {
            ProceedCreatingNormalEnemy(stageSettings);
        }

        /// <summary>
        /// 敵を生成する
        /// </summary>
        private void ProceedCreatingNormalEnemy(Controller.StageSettings stageSettings)
        {
            // 敵の数の上限が0だったら抜ける
            // まだ始まってなかったら抜ける
            // ボス出現演出中だったら抜ける
            // ボスが死んだら抜ける
            if (stageSettings.maxEnemyAmount == 0
                || !_stageStatusManager.isGameGoing
                || _stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossAppearing
                || _stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossDying
                || _stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossDead)
                return;

            // 敵を生成するかどうかを確率で決める
            // (この部分単体だと、敵の数の上限が0の時うまく動作しない)
            if (Random.value > stageSettings.enemyProduceProbabilityCurve.Evaluate(
                                (float)totalEnemyAmount / (float)stageSettings.maxEnemyAmount) * Time.deltaTime)
                return;

            // 生成する敵をランダムに選んで、対応する辞書の値を変更する
            CreateNormalEnemy(RandomChoosing.ChooseRandomly(_produceProbabilityRatios));

            totalEnemyAmount++;
        }

        // 敵を生成する
        public void CreateNormalEnemy(Controller.NormalEnemyData.normalEnemyType type)
        {
            enemyNumbers[(int)type]++;
        }
    }
}
