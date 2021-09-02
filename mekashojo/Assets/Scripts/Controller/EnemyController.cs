using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public struct EnemyElements
    {
        public Model.Enemy__SimpleBullet enemy__SimpleBullet;
        public Model.Enemy__SpreadBullet enemy__SpreadBullet;
        public Model.Enemy__WideSpreadBullet enemy__WideSpreadBullet;
        public Model.Enemy__WideBeam enemy__WideBeam;
        public Model.Enemy__SelfDestruct enemy__SelfDestruct;

        public GameObject enemyObject;
    }

    public struct EnemyFireElements
    {
        public Model.EnemyFire enemyFire;
        public GameObject enemyFireObject;
    }

    public class EnemyController : MonoBehaviour
    {
        public static Dictionary<int, Model.EnemyDamageManager> damageManagerTable;
        public static Dictionary<enemyType__Rough, Dictionary<int, EnemyElements>> enemyTable;
        public static Dictionary<int, EnemyFireElements> fireTable__Bullet;

        private GameObject _player;

        public enum enemyType__Rough
        {
            SimpleBullet,
            SpreadBullet,
            WideSpreadBullet,
            WideBeam,
            SelfDestruct,
        }

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("BattleScenes/Player");

            damageManagerTable = new Dictionary<int, Model.EnemyDamageManager>();
            enemyTable = new Dictionary<enemyType__Rough, Dictionary<int, EnemyElements>>()
            {
                { enemyType__Rough.SimpleBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.SpreadBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.WideSpreadBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.WideBeam, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.SelfDestruct, new Dictionary<int, EnemyElements>() },
            };
            fireTable__Bullet = new Dictionary<int, EnemyFireElements>();
        }

        // Update is called once per frame
        void Update()
        {
            // EnemyDamageManagerの処理
            foreach (Model.EnemyDamageManager enemyDamageManager in damageManagerTable.Values)
            {
                enemyDamageManager.RunEveryFrame();
            }

            // EnemyTableの処理
            foreach (KeyValuePair<enemyType__Rough, Dictionary<int, EnemyElements>> pair in enemyTable)
            {
                switch (pair.Key)
                {
                    // Enemy__SimpleBulletの処理
                    case enemyType__Rough.SimpleBullet:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__SimpleBullet.RunEveryFrame(
                                enemyElements.enemyObject.transform.position,
                                _player.transform.position
                                );
                        }
                        break;

                    // Enemy__SpreadBulletの処理
                    case enemyType__Rough.SpreadBullet:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__SpreadBullet.RunEveryFrame(
                                enemyElements.enemyObject.transform.position
                                );
                        }
                        break;

                    // Enemy__WideSpreadBulletの処理
                    case enemyType__Rough.WideSpreadBullet:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__WideSpreadBullet.RunEveryFrame(
                                enemyElements.enemyObject.transform.position
                                );
                        }
                        break;

                    // Enemy__WideBeamの処理
                    case enemyType__Rough.WideBeam:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__WideBeam.RunEveryFrame(
                                enemyElements.enemyObject.transform.position
                                );
                        }
                        break;

                    // Enemy__SelfDestructの処理
                    case enemyType__Rough.SelfDestruct:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__SelfDestruct.RunEveryFrame(
                                enemyElements.enemyObject.transform.position
                                );
                        }
                        break;
                }
            }

            // EnemyFire__Bulletの処理
            foreach (EnemyFireElements enemyFireElements in fireTable__Bullet.Values)
            {
                enemyFireElements.enemyFire.RunEveryFrame(
                    enemyFireElements.enemyFireObject.transform.position,
                    _player.transform.position
                    );
            }
        }

        /// <summary>
        /// View.Enemy__SimpleBulletのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemy__SimpleBullet(NormalEnemyData normalEnemyData, GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(BattleScenesController.enemyManager, normalEnemyData);

            Model.Enemy__SimpleBullet enemy__SimpleBullet
                = new Model.Enemy__SimpleBullet(
                    BattleScenesController.pauseManager,
                    BattleScenesController.playerStatusManager,
                    BattleScenesController.enemyManager,
                    normalEnemyData
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__SimpleBullet = enemy__SimpleBullet,
                enemyObject = enemyObject,
            };

            int id = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.SimpleBullet].Add(id, enemyElements);
            damageManagerTable.Add(id, enemyDamageManager);

            return id;
        }

        /// <summary>
        /// View.Enemy__SpreadBulletのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemy__SpreadBullet(NormalEnemyData normalEnemyData, GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(BattleScenesController.enemyManager, normalEnemyData);

            Model.Enemy__SpreadBullet enemy__SpreadBullet
                = new Model.Enemy__SpreadBullet(
                    BattleScenesController.pauseManager,
                    BattleScenesController.playerStatusManager,
                    BattleScenesController.enemyManager,
                    normalEnemyData
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__SpreadBullet = enemy__SpreadBullet,
                enemyObject = enemyObject,
            };

            int id = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.SpreadBullet].Add(id, enemyElements);
            damageManagerTable.Add(id, enemyDamageManager);

            return id;
        }

        /// <summary>
        /// View.Enemy__WideSpreadBulletのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemy__WideSpreadBullet(NormalEnemyData normalEnemyData, GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(BattleScenesController.enemyManager, normalEnemyData);

            Model.Enemy__WideSpreadBullet enemy__WideSpreadBullet
                = new Model.Enemy__WideSpreadBullet(
                    BattleScenesController.pauseManager,
                    BattleScenesController.playerStatusManager,
                    BattleScenesController.enemyManager,
                    normalEnemyData
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__WideSpreadBullet = enemy__WideSpreadBullet,
                enemyObject = enemyObject,
            };

            int id = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.WideSpreadBullet].Add(id, enemyElements);
            damageManagerTable.Add(id, enemyDamageManager);

            return id;
        }

        /// <summary>
        /// View.Enemy__WideBeamのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemy__WideBeam(NormalEnemyData normalEnemyData, GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(BattleScenesController.enemyManager, normalEnemyData);

            Model.Enemy__WideBeam enemy__WideBeam
                = new Model.Enemy__WideBeam(
                    BattleScenesController.pauseManager,
                    BattleScenesController.playerStatusManager,
                    BattleScenesController.enemyManager,
                    normalEnemyData
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__WideBeam = enemy__WideBeam,
                enemyObject = enemyObject,
            };

            int id = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.WideBeam].Add(id, enemyElements);
            damageManagerTable.Add(id, enemyDamageManager);

            return id;
        }

        /// <summary>
        /// View.Enemy__SelfDestructのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemy__SelfDestruct(NormalEnemyData normalEnemyData, GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(BattleScenesController.enemyManager, normalEnemyData);

            Model.Enemy__SelfDestruct enemy__SelfDestruct
                = new Model.Enemy__SelfDestruct(
                    BattleScenesController.pauseManager,
                    BattleScenesController.playerStatusManager,
                    BattleScenesController.enemyManager,
                    normalEnemyData
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__SelfDestruct = enemy__SelfDestruct,
                enemyObject = enemyObject,
            };

            int id = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.SelfDestruct].Add(id, enemyElements);
            damageManagerTable.Add(id, enemyDamageManager);

            return id;
        }

        /// <summary>
        /// View.EnemyFireのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemyFire(NormalEnemyData normalEnemyData, GameObject enemyFireObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyFire enemyFire = new Model.EnemyFire(
                normalEnemyData,
                BattleScenesController.enemyManager,
                BattleScenesController.playerStatusManager,
                BattleScenesController.playerPositionManager,
                BattleScenesController.pauseManager
                );

            EnemyFireElements enemyFireElements
                = new EnemyFireElements()
                {
                    enemyFire = enemyFire,
                    enemyFireObject = enemyFireObject,
                };

            int id = IDManager.GetEnemyBulletID();

            fireTable__Bullet.Add(id, enemyFireElements);

            return id;
        }
    }
}
