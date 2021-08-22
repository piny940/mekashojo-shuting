using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Model
{
    public class EnemyController
    {
        private Dictionary<NormalEnemyData.normalEnemyType, float> _produceProbabilityRatios; //各敵を生成する確率の生成比

        private PauseController _pauseController;

        public int totalEnemyAmount { get; set; }   //今いる敵の数
        public static readonly float enemyPosition__z = 10;

        //生成した敵の数(今ステージ上にいる敵の数とは無関係)
        public ObservableCollection<int> enemyNumbers { get; private set; }
            = new ObservableCollection<int>(new int[System.Enum.GetNames(typeof(NormalEnemyData.normalEnemyType)).Length]);

        public EnemyController(PauseController pauseController, EnemyControlData enemyControlData)
        {
            _pauseController = pauseController;

            _produceProbabilityRatios = new Dictionary<NormalEnemyData.normalEnemyType, float>()
            {
                { NormalEnemyData.normalEnemyType.SpreadBullet, enemyControlData.spreadBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.SingleBullet, enemyControlData.singleBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.StunBullet, enemyControlData.stunBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.FastBullet, enemyControlData.fastBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.SlowBullet, enemyControlData.slowBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.Missile, enemyControlData.missileEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.RepeatedFire, enemyControlData.repeatedEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.WideBeam, enemyControlData.wideBeamEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.GuidedBullet, enemyControlData.guidedBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.WideSpreadBullet, enemyControlData.wideSpreadBulletEnemyProduceProbabilityRatio },
                { NormalEnemyData.normalEnemyType.SelfDestruct, enemyControlData.selfDestructEnemyProduceProbabilityRatio },
            };

            //はじめにいる敵の数
            totalEnemyAmount = enemyControlData.firstEnemyAmount;
        }

        /// <summary>
        /// 敵を生成する
        /// </summary>
        public void CreateNewEnemy(EnemyControlData enemyControlData)
        {
            //まだ始まってなかったら抜ける
            if (!_pauseController.isGameGoing)
                return;

            //敵を生成するかどうかを確率で決める
            //この行はどこで改行するのが見やすい？？
            if (Random.value > enemyControlData.enemyProduceProbabilityCurve.Evaluate((float)totalEnemyAmount / (float)enemyControlData.maxEnemyAmount) * Time.deltaTime)
                return;

            // 生成する敵をランダムに選んで、対応する辞書の値を変更する
            enemyNumbers[(int)RandomChoosing.ChooseRandomly(_produceProbabilityRatios)] += 1;

            totalEnemyAmount++;
        }
    }
}
