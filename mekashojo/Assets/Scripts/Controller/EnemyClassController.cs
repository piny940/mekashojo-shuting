using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public struct EnemyElements
    {
        public Model.Enemy__SimpleBullet enemy__SimpleBullet;
        public Model.Enemy__WideBeam enemy__WideBeam;

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
        public static Dictionary<int, EnemyElements> enemyTable__WideBeam;
        public static Dictionary<int, EnemyFireElements> fireTable__Bullet;

        public static GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("BattleScenes/Player");

            damageManagerTable = new Dictionary<int, Model.EnemyDamageManager>();
            enemyTable__SimpleBullet = new Dictionary<int, EnemyElements>();
            enemyTable__WideBeam = new Dictionary<int, EnemyElements>();
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
                    player.transform.position,
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

            // EnemyFire__Bulletの処理
            foreach (EnemyFireElements enemyFireElements in fireTable__Bullet.Values)
            {
                if (enemyFireElements.enemyFire == null)
                    return;

                enemyFireElements.enemyFire.DestroyLater(
                    enemyFireElements.enemyFireObject.transform.position
                    );

                enemyFireElements.enemyFire.StopOnPausing();
            }
        }
    }
}
