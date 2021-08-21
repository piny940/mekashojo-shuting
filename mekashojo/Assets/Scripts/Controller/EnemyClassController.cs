using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public struct EnemyElements
    {
        public Model.Enemy__SimpleBullet enemy__SimpleBullet;
        public Model.Enemy__WideBeam enemy__WideBeam;

        public Model.EnemyDamageManager enemyDamageManager;
        public GameObject enemyObject;
    }

    public struct EnemyFireElements
    {
        public Model.EnemyFire enemyFire;
        public GameObject enemyFireObject;
    }

    public class EnemyClassController : MonoBehaviour
    {
        public static Dictionary<int, EnemyElements> enemyTable__SimpleBullet;
        public static Dictionary<int, EnemyElements> enemyTable__WideBeam;
        public static Dictionary<int, EnemyFireElements> enemyFireTable__Bullet;

        public static GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("BattleScenes/Player");

            enemyTable__SimpleBullet = new Dictionary<int, EnemyElements>();
            enemyTable__WideBeam = new Dictionary<int, EnemyElements>();
            enemyFireTable__Bullet = new Dictionary<int, EnemyFireElements>();
        }

        // Update is called once per frame
        void Update()
        {
            // Enemy__SimpleBulletの処理
            foreach (EnemyElements enemyElements in enemyTable__SimpleBullet.Values)
            {
                if (enemyElements.enemy__SimpleBullet == null)
                    return;

                enemyElements.enemy__SimpleBullet.RunEveryFrame(
                    player.transform.position,
                    enemyElements.enemyObject.transform.position
                    );

                enemyElements.enemyDamageManager.CountFrameForPlayerBomb();
            }

            // Enemy__WideBeamの処理
            foreach (EnemyElements enemyElements in enemyTable__WideBeam.Values)
            {
                if (enemyElements.enemy__WideBeam == null)
                    return;

                enemyElements.enemy__WideBeam.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );

                enemyElements.enemyDamageManager.CountFrameForPlayerBomb();
            }

            // EnemyFire__Bulletの処理
            foreach (EnemyFireElements enemyFireElements in enemyFireTable__Bullet.Values)
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
