using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Model
{
    public class EnemyManager
    {
        private Dictionary<Controller.NormalEnemyData.normalEnemyType, float> _produceProbabilityRatios; //各敵を生成する確率の生成比

        private PauseManager _pauseManager;

        public int totalEnemyAmount { get; set; }   //今いる敵の数
        public static readonly float enemyPosition__z = 10;

        //生成した敵の数(今ステージ上にいる敵の数とは無関係)
        //この配列の変化をViewが検知して敵を生成する
        //逆に言うと、enemyNumbersはViewに変更を通知するためだけの値であって
        //何か意味を持った値ではない
        public ObservableCollection<int> enemyNumbers { get; private set; }
            = new ObservableCollection<int>(new int[System.Enum.GetNames(typeof(Controller.NormalEnemyData.normalEnemyType)).Length]);

        public EnemyManager(PauseManager pauseManager, Controller.EnemyControlData enemyControlData)
        {
            _pauseManager = pauseManager;

            _produceProbabilityRatios = new Dictionary<Controller.NormalEnemyData.normalEnemyType, float>()
            {
                { Controller.NormalEnemyData.normalEnemyType.SpreadBullet, enemyControlData.spreadBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SingleBullet, enemyControlData.singleBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.StunBullet, enemyControlData.stunBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.FastBullet, enemyControlData.fastBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SlowBullet, enemyControlData.slowBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.Missile, enemyControlData.missileEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.RepeatedFire, enemyControlData.repeatedEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.WideBeam, enemyControlData.wideBeamEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.GuidedBullet, enemyControlData.guidedBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.WideSpreadBullet, enemyControlData.wideSpreadBulletEnemyProduceProbabilityRatio },
                { Controller.NormalEnemyData.normalEnemyType.SelfDestruct, enemyControlData.selfDestructEnemyProduceProbabilityRatio },
            };

            //はじめにいる敵の数
            totalEnemyAmount = enemyControlData.firstEnemyAmount;
        }

        public void RunEveryFrame(Controller.EnemyControlData enemyControlData)
        {
            CreateNewEnemy(enemyControlData);
        }

        /// <summary>
        /// 敵を生成する
        /// </summary>
        private void CreateNewEnemy(Controller.EnemyControlData enemyControlData)
        {
            //まだ始まってなかったら抜ける
            if (!_pauseManager.isGameGoing)
                return;

            //敵を生成するかどうかを確率で決める
            if (Random.value > enemyControlData.enemyProduceProbabilityCurve.Evaluate(
                                (float)totalEnemyAmount / (float)enemyControlData.maxEnemyAmount) * Time.deltaTime)
                return;

            // 生成する敵をランダムに選んで、対応する辞書の値を変更する
            enemyNumbers[(int)RandomChoosing.ChooseRandomly(_produceProbabilityRatios)]++;

            totalEnemyAmount++;
        }
    }
}
