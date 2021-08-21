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

    public class EnemyClassController : MonoBehaviour
    {
        public static Dictionary<int, EnemyElements> enemyElements__SimpleBullet;
        public static Dictionary<int, EnemyElements> enemyElements__WideBeam;
        public static GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("BattleScenes/Player");

            enemyElements__SimpleBullet = new Dictionary<int, EnemyElements>();
            enemyElements__WideBeam = new Dictionary<int, EnemyElements>();
        }

        // Update is called once per frame
        void Update()
        {
            // Enemy__SimpleBulletの処理
            foreach (EnemyElements enemyElements in enemyElements__SimpleBullet.Values)
            {
                enemyElements.enemy__SimpleBullet.RunEveryFrame(
                    player.transform.position,
                    enemyElements.enemyObject.transform.position
                    );

                enemyElements.enemyDamageManager.CountFrameForPlayerBomb();
            }

            // Enemy__WideBeamの処理
            foreach (EnemyElements enemyElements in enemyElements__WideBeam.Values)
            {
                enemyElements.enemy__WideBeam.RunEveryFrame(
                    enemyElements.enemyObject.transform.position
                    );

                enemyElements.enemyDamageManager.CountFrameForPlayerBomb();
            }
        }
    }
}
