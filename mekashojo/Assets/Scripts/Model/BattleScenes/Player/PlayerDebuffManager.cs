using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PlayerDebuffManager
    {
        private bool _isStuned = false;
        private float _powerReductionRate = 1;
        private float _speedReductionRate = 1;
        private float _shieldReductionRate = 1;
        private int _lastDebuffID = 0;

        private StageStatusManager _stageStatusManager;

        private Dictionary<debuffTypes, Dictionary<int, float>> _debuffs
            = new Dictionary<debuffTypes, Dictionary<int, float>>()
            {
                { debuffTypes.PowerReduction, new Dictionary<int, float>() },
                { debuffTypes.SpeedReduction, new Dictionary<int, float>() },
                { debuffTypes.Stun, new Dictionary<int, float>() },
                { debuffTypes.ShieldReduction, new Dictionary<int, float>() },
            };

        private Dictionary<debuffTypes, Dictionary<int, float>> _debuffTimes
            = new Dictionary<debuffTypes, Dictionary<int, float>>()
            {
                { debuffTypes.PowerReduction, new Dictionary<int, float>() },
                { debuffTypes.SpeedReduction, new Dictionary<int, float>() },
                { debuffTypes.Stun, new Dictionary<int, float>() },
                { debuffTypes.ShieldReduction, new Dictionary<int, float>() },
            };

        public UnityEvent<bool> OnIsStunedChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnPowerReductionRateChanged = new UnityEvent<float>();
        public UnityEvent<float> OnSpeedReductionRateChanged = new UnityEvent<float>();
        public UnityEvent<float> OnShieldReductionRateChanged = new UnityEvent<float>();

        public bool isStunned
        {
            get { return _isStuned; }
            set
            {
                _isStuned = value;
                OnIsStunedChanged?.Invoke(value);
            }
        }

        public float powerReductionRate
        {
            get { return _powerReductionRate; }
            private set
            {
                _powerReductionRate = value;
                OnPowerReductionRateChanged?.Invoke(value);
            }
        }

        public float speedReductionRate
        {
            get { return _speedReductionRate; }
            set
            {
                _speedReductionRate = value;
                OnSpeedReductionRateChanged?.Invoke(value);
            }
        }

        public float shieldReductionRate
        {
            get { return _shieldReductionRate; }
            set
            {
                _shieldReductionRate = value;
                OnShieldReductionRateChanged?.Invoke(value);
            }
        }

        public enum debuffTypes
        {
            Stun,
            PowerReduction,
            SpeedReduction,
            ShieldReduction,
        }

        public PlayerDebuffManager(StageStatusManager stageStatusManager)
        {
            _stageStatusManager = stageStatusManager;
        }

        public void RunEveryFrame()
        {
            ProceedDebuff();
        }

        private void ProceedDebuff()
        {
            if (!_stageStatusManager.isGameGoing) return;

            // 各デバフに対して経過時間を測り、その経過時間が効果時間より大きくなったら解除する
            foreach (debuffTypes type in System.Enum.GetValues(typeof(debuffTypes)))
                for (int id = 0; id <= _lastDebuffID; id++)
                {
                    // foreachだと辞書の編集ができないのでfor文を代用
                    // 辞書にないid値が来たら何もしない
                    if (!_debuffs[type].ContainsKey(id)) continue;

                    _debuffTimes[type][id] -= Time.deltaTime;

                    if (_debuffTimes[type][id] < 0)
                        RemoveDebuff(type, id);
                }
        }

        // デバフを追加する
        // スタンを追加する場合はreductionRateは追加しなくても良い
        public int AddDebuff(debuffTypes debuffType, float debuffTime, float reductionRate = -1)
        {
            _lastDebuffID++;
            int id = _lastDebuffID;

            // デバフのテーブルに追加をする
            _debuffs[debuffType].Add(id, reductionRate);
            _debuffTimes[debuffType].Add(id, debuffTime);

            UpdateDebuff(debuffType);
            return id;
        }

        // デバフを解除する
        public void RemoveDebuff(debuffTypes debuffType, int id = -1, bool isAll = false)
        {
            // 全て解除するかidを指定して解除するかのどちらかにしないといけない
            if (id < 0 && !isAll) throw new System.Exception();

            // 全て解除する場合
            if (isAll)
            {
                _debuffs[debuffType] = new Dictionary<int, float>();
                _debuffTimes[debuffType] = new Dictionary<int, float>();
                UpdateDebuff(debuffType);
                return;
            }

            // idを指定して解除する場合
            _debuffs[debuffType].Remove(id);
            _debuffTimes[debuffType].Remove(id);
            UpdateDebuff(debuffType);
        }

        // デバフを追加/解除した時に呼ぶ
        private void UpdateDebuff(debuffTypes debuffType)
        {
            switch (debuffType)
            {
                case debuffTypes.Stun:
                    isStunned = _debuffs[debuffTypes.Stun].Count > 0;
                    break;

                case debuffTypes.PowerReduction:
                    powerReductionRate = 1;
                    foreach (float rate in _debuffs[debuffTypes.PowerReduction].Values)
                        powerReductionRate *= rate;
                    break;

                case debuffTypes.SpeedReduction:
                    speedReductionRate = 1;
                    foreach (float rate in _debuffs[debuffTypes.SpeedReduction].Values)
                        speedReductionRate *= rate;
                    break;

                case debuffTypes.ShieldReduction:
                    shieldReductionRate = 1;
                    foreach (float rate in _debuffs[debuffTypes.ShieldReduction].Values)
                        shieldReductionRate *= rate;
                    break;
            }
        }
    }
}
