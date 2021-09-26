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
        public Model.Enemy__Boss1 enemy__Boss1;
        public Model.Enemy__Boss2 enemy__Boss2;
        public Model.Enemy__Boss3 enemy__Boss3;
        public Model.Enemy__Boss4 enemy__Boss4;
        public Model.Enemy__LastBoss enemy__LastBoss;

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
        public static int bossID { get; private set; }

        private GameObject _player;

        public enum enemyType__Rough
        {
            SimpleBullet,
            SpreadBullet,
            WideSpreadBullet,
            WideBeam,
            SelfDestruct,
            Boss1,
            Boss2,
            Boss3,
            Boss4,
            LastBoss,
        }

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag(View.TagManager.TagNames.BattleScenes__Player.ToString());

            damageManagerTable = new Dictionary<int, Model.EnemyDamageManager>();

            enemyTable = new Dictionary<enemyType__Rough, Dictionary<int, EnemyElements>>()
            {
                { enemyType__Rough.SimpleBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.SpreadBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.WideSpreadBullet, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.WideBeam, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.SelfDestruct, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.Boss1, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.Boss2, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.Boss3, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.Boss4, new Dictionary<int, EnemyElements>() },
                { enemyType__Rough.LastBoss, new Dictionary<int, EnemyElements>() },
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

                    // Enemy__Boss1の処理
                    case enemyType__Rough.Boss1:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__Boss1.RunEveryFrame(
                                enemyElements.enemyObject.transform.position,
                                _player.transform.position
                                );
                        }
                        break;

                    // Enemy__Boss2の処理
                    case enemyType__Rough.Boss2:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__Boss2.RunEveryFrame();
                        }
                        break;

                    // Enemy__Boss3の処理
                    case enemyType__Rough.Boss3:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__Boss3.RunEveryFrame();
                        }
                        break;


                    // Enemy__Boss4の処理
                    case enemyType__Rough.Boss4:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__Boss4.RunEveryFrame();
                        }
                        break;

                    // Enemy__LastBossの処理
                    case enemyType__Rough.LastBoss:
                        foreach (EnemyElements enemyElements in pair.Value.Values)
                        {
                            enemyElements.enemy__LastBoss.RunEveryFrame();
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
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    normalEnemyData.hp
                    );

            Model.Enemy__SimpleBullet enemy__SimpleBullet
                = new Model.Enemy__SimpleBullet(
                    BattleScenesController.stageStatusManager,
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
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    normalEnemyData.hp
                    );

            Model.Enemy__SpreadBullet enemy__SpreadBullet
                = new Model.Enemy__SpreadBullet(
                    BattleScenesController.stageStatusManager,
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
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    normalEnemyData.hp);

            Model.Enemy__WideSpreadBullet enemy__WideSpreadBullet
                = new Model.Enemy__WideSpreadBullet(
                    BattleScenesController.stageStatusManager,
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
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    normalEnemyData.hp
                    );

            Model.Enemy__WideBeam enemy__WideBeam
                = new Model.Enemy__WideBeam(
                    BattleScenesController.stageStatusManager,
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
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    normalEnemyData.hp
                    );

            Model.Enemy__SelfDestruct enemy__SelfDestruct
                = new Model.Enemy__SelfDestruct(
                    BattleScenesController.stageStatusManager,
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
        /// View.Enemy__Boss1のStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// </summary>
        public static void EmergeEnemy__Boss1(GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.Enemy__Boss1 enemy__Boss1
                = new Model.Enemy__Boss1(
                    BattleScenesController.stageStatusManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.playerStatusManager
                    );

            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.Enemy__Boss1.maxHP,
                    true
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__Boss1 = enemy__Boss1,
                enemyObject = enemyObject,
            };

            bossID = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.Boss1].Add(bossID, enemyElements);
            damageManagerTable.Add(bossID, enemyDamageManager);
        }

        /// <summary>
        /// View.Enemy__Boss2のStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// </summary>
        public static void EmergeEnemy__Boss2(GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.Enemy__Boss2 enemy__Boss2
                = new Model.Enemy__Boss2(
                    BattleScenesController.stageStatusManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.playerStatusManager
                    );

            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.Enemy__Boss2.maxHP,
                    true
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__Boss2 = enemy__Boss2,
                enemyObject = enemyObject,
            };

            bossID = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.Boss2].Add(bossID, enemyElements);
            damageManagerTable.Add(bossID, enemyDamageManager);
        }

        /// <summary>
        /// View.Enemy__Boss3のStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// </summary>
        public static void EmergeEnemy__Boss3(GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.Enemy__Boss3 enemy__Boss3
                = new Model.Enemy__Boss3(
                    BattleScenesController.stageStatusManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.playerStatusManager
                    );

            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.Enemy__Boss3.maxHP,
                    true
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__Boss3 = enemy__Boss3,
                enemyObject = enemyObject,
            };

            bossID = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.Boss3].Add(bossID, enemyElements);
            damageManagerTable.Add(bossID, enemyDamageManager);
        }

        /// <summary>
        /// View.Enemy__Boss4のStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// </summary>
        public static void EmergeEnemy__Boss4(GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.Enemy__Boss4.maxHP,
                    true
                    );

            Model.Enemy__Boss4 enemy__Boss4
                = new Model.Enemy__Boss4(
                    enemyDamageManager,
                    BattleScenesController.playerDebuffManager,
                    BattleScenesController.stageStatusManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.playerStatusManager
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__Boss4 = enemy__Boss4,
                enemyObject = enemyObject,
            };

            bossID = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.Boss4].Add(bossID, enemyElements);
            damageManagerTable.Add(bossID, enemyDamageManager);
        }

        /// <summary>
        /// View.Enemy__LastBossのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// </summary>
        public static void EmergeEnemy__LastBoss(GameObject enemyObject)
        {
            // Modelクラスのインスタンスを作成
            Model.Enemy__LastBoss enemy__LastBoss
                = new Model.Enemy__LastBoss(
                    BattleScenesController.stageStatusManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.playerStatusManager
                    );

            Model.EnemyDamageManager enemyDamageManager
                = new Model.EnemyDamageManager(
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.Enemy__LastBoss.maxHP,
                    true
                    );

            EnemyElements enemyElements = new EnemyElements()
            {
                enemy__LastBoss = enemy__LastBoss,
                enemyObject = enemyObject,
            };

            bossID = IDManager.GetEnemyID();

            enemyTable[enemyType__Rough.LastBoss].Add(bossID, enemyElements);
            damageManagerTable.Add(bossID, enemyDamageManager);
        }

        /// <summary>
        /// 敵の弾のビュークラスのStartメソッドで呼ぶ<br></br>
        /// モデルクラスのインスタンスを作成<br></br>
        /// IDを取得して返す
        /// </summary>
        public static int EmergeEnemyBullet(Model.EnemyFire.FireInfo fireInfo, GameObject enemyFireObject)
        {
            // Modelクラスのインスタンスを作成
            Model.EnemyFire enemyFire = new Model.EnemyFire(
                fireInfo,
                BattleScenesController.enemyManager,
                BattleScenesController.playerDebuffManager,
                BattleScenesController.playerStatusManager,
                BattleScenesController.shield__Player,
                BattleScenesController.stageStatusManager
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
