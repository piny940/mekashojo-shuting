using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__WideBeam : DamageFactorManager
    {
        private float _time = 0;
        private float _attackingTime = 0;
        private bool _isAttacking = false;
        private beamFiringProcesses _beamStatus = beamFiringProcesses.HasStoppedBeam;
        private Controller.NormalEnemyData _normalEnemyData;
        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public UnityEvent<beamFiringProcesses> OnBeamStatusChanged
            = new UnityEvent<beamFiringProcesses>();

        public beamFiringProcesses beamStatus
        {
            get { return _beamStatus; }
            set
            {
                _beamStatus = value;
                OnBeamStatusChanged?.Invoke(_beamStatus);
            }
        }

        public Enemy__WideBeam(StageStatusManager stageStatusManager, PlayerStatusManager playerStatusManager, EnemyManager enemyManager, Controller.NormalEnemyData normalEnemyData)
                : base(stageStatusManager, enemyManager, playerStatusManager)
        {
            _normalEnemyData = normalEnemyData;
            _time = Random.value * _normalEnemyData.firingInterval;
            factorType = DamageFactorData.damageFactorType.FiringNormalEnemy;
        }

        public void RunEveryFrame(Vector3 position)
        {
            ProceedAttack();
            DisappearIfOutside(position);
            StopOnPausing();
            SetConstantVelocity(_normalEnemyData.movingSpeed);
        }

        protected override void ChangeBeamStatus(beamFiringProcesses status)
        {
            beamStatus = status;
        }

        private void ProceedAttack()
        {
            if (!stageStatusManager.isGameGoing
                || stageStatusManager.currentStageStatus == StageStatusManager.stageStatus.BossAppearing)
                return;

            _time += Time.deltaTime;

            // 攻撃をやめる処理
            if (!_isAttacking && _attackingTime > 0)
            {
                _attackingTime = 0;
                return;
            }

            // 攻撃を始める処理
            if (_time > _normalEnemyData.firingInterval && !_isAttacking)
            {
                _isAttacking = true;
                _time = 0;
            }

            // 攻撃本体
            if (_isAttacking)
            {
                _isAttacking = ProceedBeamFiring(_normalEnemyData.beamNotifyingTime, _normalEnemyData.beamTime);
                _attackingTime += Time.deltaTime;
            }
        }
    }
}
