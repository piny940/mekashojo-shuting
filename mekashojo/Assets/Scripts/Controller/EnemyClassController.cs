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

    public class EnemyClassController : MonoBehaviour
    {
        public static Dictionary<int, Model.EnemyDamageManager> damageManagerTable;
        public static Dictionary<int, EnemyElements> enemyTable__SimpleBullet;
        public static Dictionary<int, EnemyElements> enemyTable__SpreadBullet;
        public static Dictionary<int, EnemyElements> enemyTable__WideSpreadBullet;
        public static Dictionary<int, EnemyElements> enemyTable__WideBeam;
        public static Dictionary<int, EnemyElements> enemyTable__SelfDestruct;
        public static Dictionary<int, EnemyFireElements> fireTable__Bullet;

        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("BattleScenes/Player");

            damageManagerTable = new Dictionary<int, Model.EnemyDamageManager>();
            enemyTable__SimpleBullet = new Dictionary<int, EnemyElements>();
            enemyTable__SpreadBullet = new Dictionary<int, EnemyElements>();
            enemyTable__WideSpreadBullet = new Dictionary<int, EnemyElements>();
            enemyTable__WideBeam = new Dictionary<int, EnemyElements>();
            enemyTable__SelfDestruct = new Dictionary<int, EnemyElements>();
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

            // Enemy__SimpleBulletの処理
            foreach (EnemyElements enemyElements in enemyTable__SimpleBullet.Values)
            {
                enemyElements.enemy__SimpleBullet.RunEveryFrame(
                    _player.transform.position,
                    enemyElements.enemyObject.transform.position
                    );
            }

            // Enemy__SpreadBulletの処理
            foreach (EnemyElements enemyElements in enemyTable__SpreadBullet.Values)
            {
                enemyElements.enemy__SpreadBullet.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );
            }

            // Enemy__WideSpreadBulletの処理
            foreach (EnemyElements enemyElements in enemyTable__WideSpreadBullet.Values)
            {
                enemyElements.enemy__WideSpreadBullet.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );
            }

            // Enemy__WideBeamの処理
            foreach (EnemyElements enemyElements in enemyTable__WideBeam.Values)
            {
                enemyElements.enemy__WideBeam.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );
            }

            // Enemy__SelfDestructの処理
            foreach (EnemyElements enemyElements in enemyTable__SelfDestruct.Values)
            {
                enemyElements.enemy__SelfDestruct.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );
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
    }
}
