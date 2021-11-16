using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
                { DropMaterialManager.materialType.EnergyChargeMaterial, 0.05f },
                { DropMaterialManager.materialType.BombChargeMaterial, 0.04f },
            };

        private EnemyManager _enemyManager;
        private StageStatusManager _stageStatusManager;

        private bool _isDying = false;

        private bool _isBoss = false;

        private float _hp;

        public readonly int noBombDamageFrames = 3;

        //BombFire__Playerで"frameCounterForPlayerBombがnoBombDamageFramesより小さかったらダメージを受けない
        public int frameCounterForPlayerBomb { get; private set; }

        public float damageReductionRate { get; set; }

        public UnityEvent<bool> OnIsDyingChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnHPChanged = new UnityEvent<float>();

        public ObservableCollection<int> materialNumbers
            = new ObservableCollection<int>(new int[10]);

        public bool isDying
        {
            get { return _isDying; }
            set
            {
                _isDying = value;
                OnIsDyingChanged?.Invoke(_isDying);
            }
        }

        public float hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnHPChanged?.Invoke(value);
            }
        }

        public EnemyDamageManager(EnemyManager enemyManager, StageStatusManager stageStatusManager, float hp, bool isBoss = false)
        {
            _enemyManager = enemyManager;
            _stageStatusManager = stageStatusManager;
            this.hp = hp;
            _isBoss = isBoss;
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        /// <param name="power"></param>
        public void GetDamage(float power)
        {
            hp -= power * (1 - damageReductionRate);

            if (hp <= 0 && !isDying)
            {
                Die();
            }
        }

        public void RunEveryFrame()
        {
            CountFrameForPlayerBomb();
        }

        /// <summary>
        /// 死ぬ
        /// </summary>
        private void Die()
        {
            if (_isBoss)
            {
                _stageStatusManager.ChangeStatus(StageStatusManager.stageStatus.BossDying);
                return;
            }

            //ドロップアイテムを落とす
            if (Random.value < _droppingProbabilities.Values.Sum())
            {
                materialNumbers[(int)RandomChoosing.ChooseRandomly(_droppingProbabilities)]++;
            }

            //消滅する
            _enemyManager.totalEnemyAmount--;

            isDying = true;
        }

        /// <summary>
        /// プレイヤーのボムの内側にスポーンした時はボムをダメージを受けないようにするためのフレームカウンター<br></br>
        /// 「frameCounterForPlayerBomb」をマイフレームincrementする
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
