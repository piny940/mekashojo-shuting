using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.ObjectModel;

namespace Model
{
    public class EnemyDamageManager
    {
        private Dictionary<DropMaterialManager.materialType, float> _droppingProbabilities
            = new Dictionary<DropMaterialManager.materialType, float>()
            {
                { DropMaterialManager.materialType.CannonEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.LaserEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.BeamMachineGunEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.BalkanEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.MissileEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.BombEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.HeavyShieldEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.LightShieldEnhancementMaterial, 0.025f },
                { DropMaterialManager.materialType.EnergyChargeMaterial, 0.15f },
                { DropMaterialManager.materialType.BombChargeMaterial, 0.1f },
            };

        private EnemyController _enemyController;

        private bool _isDead = false;

        public float hp { get; set; }
        public readonly int noBombDamageFrames = 3;

        //BombFire__Playerで"frameCounterForPlayerBombがnoBombDamageFramesより小さかったらダメージを受けない
        public int frameCounterForPlayerBomb { get; private set; }

        public UnityEvent<bool> OnIsDeadChanged = new UnityEvent<bool>();

        public ObservableCollection<int> materialNumbers
            = new ObservableCollection<int>(new int[10]);

        public bool isDead
        {
            get { return _isDead; }
            set
            {
                _isDead = value;
                OnIsDeadChanged?.Invoke(_isDead);
            }
        }

        public EnemyDamageManager(EnemyController enemyController, NormalEnemyData normalEnemyData)
        {
            _enemyController = enemyController;
            hp = normalEnemyData.hp;
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        /// <param name="power"></param>
        public void GetDamage(float power)
        {
            hp -= power;

            if (hp <= 0 && !isDead)
            {
                Die();
            }
        }

        public void RunEveryFrame() { CountFrameForPlayerBomb(); }

        /// <summary>
        /// 死ぬ
        /// </summary>
        private void Die()
        {
            //ドロップアイテムを落とす
            if (Random.value < _droppingProbabilities.Values.Sum())
            {
                materialNumbers[(int)RandomChoosing.ChooseRandomly(_droppingProbabilities)]++;
            }

            //消滅する
            _enemyController.totalEnemyAmount--;

            isDead = true;
        }

        /// <summary>
        /// プレイヤーのボムの内側にスポーンした時はボムをダメージを受けないようにするためのフレームカウンター
        /// 「frameCounterForPlayerBomb」をマイフレームincrementする<br></br>
        /// </summary>
        private void CountFrameForPlayerBomb()
        {
            if (frameCounterForPlayerBomb < noBombDamageFrames)
            {
                frameCounterForPlayerBomb++;
            }
        }
    }
}
