using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.ObjectModel;

namespace Model
{
    public class EnemyDamageManager
    {
        private Dictionary<DropMaterialManager.MaterialType, float> _droppingProbabilities
            = new Dictionary<DropMaterialManager.MaterialType, float>()
            {
                { DropMaterialManager.MaterialType.CannonEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.LaserEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.BeamMachineGunEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.BalkanEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.MissileEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.BombEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.HeavyShieldEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.LightShieldEnhancementMaterial, 0.025f },
                { DropMaterialManager.MaterialType.EnergyChargeMaterial, 0.15f },
                { DropMaterialManager.MaterialType.BombChargeMaterial, 0.1f },
            };

        private EnemyController _enemyController;

        private bool _isDead = false;

        public float hp { get; set; }
        public readonly int noBombDamageFrames = 3;

        //BombFire__Playerで"frameCounterForPlayerBombがnoBombDamageFramesより小さかったらダメージを受けない
        public int frameCounterForPlayerBomb { get; private set; }
        public bool isInsideBomb = false;

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

            if (hp <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// 死ぬ
        /// </summary>
        void Die()
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
        /// Updateで呼ぶ
        /// </summary>
        public void CountFrameForPlayerBomb()
        {
            if (frameCounterForPlayerBomb < noBombDamageFrames)
            {
                frameCounterForPlayerBomb++;
            }
        }
    }
}
