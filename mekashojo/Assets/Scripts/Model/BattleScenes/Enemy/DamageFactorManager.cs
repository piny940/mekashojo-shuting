using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    // 雑魚敵、ステージボス、触れたらダメージを受ける壁など、プレイヤーにダメージを
    // 与えるオブジェクトのクラスはこのクラスを継承する
    public abstract class DamageFactorManager : MovingObjectBase
    {
        private bool _hasVelocitySet = false;
        private int _frameCount = 0;
        private int _firingCount = 0;
        private float _firingTime = 0;

        private FiringBulletInfo _firingBulletInfo;
        private beamFiringProcesses _beamStatus = beamFiringProcesses.HasStoppedBeam;
        private PlayerStatusController _playerStatusController;
        protected EnemyController enemyController;
        protected override movingObjectType objectType { get; set; }
        protected abstract DamageFactorData.damageFactorType factorType { get; set; }

        public UnityEvent<beamFiringProcesses> OnBeamStatusChanged = new UnityEvent<beamFiringProcesses>();
        public UnityEvent<FiringBulletInfo> OnFiringBulletInfoChanged = new UnityEvent<FiringBulletInfo>();

        public beamFiringProcesses beamStatus
        {
            get { return _beamStatus; }
            set
            {
                _beamStatus = value;
                OnBeamStatusChanged?.Invoke(_beamStatus);
            }
        }

        public FiringBulletInfo firingBulletInfo
        {
            get { return _firingBulletInfo; }
            set
            {
                _firingBulletInfo = value;
                OnFiringBulletInfoChanged?.Invoke(value);
            }
        }

        // ビームを発射するときの過程
        public enum beamFiringProcesses
        {
            IsNotifyingBeamFiring,
            IsFiringBeam,
            HasStoppedBeam,
        }

        // 弾を１つ発射するときに必要な情報
        public struct FiringBulletInfo
        {
            public Vector3 bulletVelocity;
            public string firePath;
        }

        // 弾を(しばしば複数)発射する処理をするときに必要な情報
        public struct BulletProcessInfo
        {
            public float shortInterval_Frame;
            public List<Vector3> bulletVelocities;
            public string firePath;
            public int firingAmount;
        }

        protected DamageFactorManager(PauseController pauseController, EnemyController enemyController, PlayerStatusController playerStatusController)
                : base(enemyController, pauseController)
        {
            _playerStatusController = playerStatusController;
            objectType = movingObjectType.Enemy;
        }

        /// <summary>
        /// 弾を発射するタイプの敵はこのメソッドを呼ぶ
        /// </summary>
        protected bool ProceedBulletFiring(BulletProcessInfo firingBulletInfo)
        {
            //一定数発射したら発射を終了する
            if (_firingCount > firingBulletInfo.firingAmount)
            {
                _firingCount = 0;
                return false;
            }

            _frameCount++;

            //一定小時間ごとに発射する
            if (_frameCount > firingBulletInfo.shortInterval_Frame)
            {
                _frameCount = 0;

                foreach (Vector3 bulletVelocity in firingBulletInfo.bulletVelocities)
                {
                    Debug.Log("FiringBulletInfo was newed.");
                    _firingBulletInfo = new FiringBulletInfo()
                    {
                        bulletVelocity = bulletVelocity,
                        firePath = "Enemy/EnemyFire__" + firingBulletInfo.firePath,
                    };
                }

                _firingCount++;
            }

            return true;
        }

        /// <summary>
        /// ビームを発射する敵はこのメソッドを呼ぶ
        /// 攻撃中かどうかを戻り値で返すようにしたんやけど、
        /// このメソッド、使い方が特殊すぎて属人性が高そう。改善案求む。
        /// </summary>
        protected bool ProceedBeamFiring(float beamNotifyingTime, float beamTime)
        {
            _firingTime += Time.deltaTime;

            //攻撃の予告
            if (_firingTime <= beamNotifyingTime && beamStatus != beamFiringProcesses.IsNotifyingBeamFiring)
            {
                beamStatus = beamFiringProcesses.IsNotifyingBeamFiring;
            }

            //攻撃時
            if (_firingTime > beamNotifyingTime && _firingTime <= beamNotifyingTime + beamTime && beamStatus != beamFiringProcesses.IsFiringBeam)
            {
                beamStatus = beamFiringProcesses.IsFiringBeam;
            }

            //攻撃終了時
            if (_firingTime > beamNotifyingTime + beamTime)
            {
                beamStatus = beamFiringProcesses.HasStoppedBeam;

                _firingTime = 0;

                return false;
            }

            return true;
        }

        /// <summary>
        /// 移動速度の設定(移動速度が一定の場合)
        /// </summary>
        protected void SetConstantVelocity(float movingSpeed)
        {
            //まだ始まってなかったら抜ける
            if (!pauseController.isGameGoing) return;

            if (!_hasVelocitySet)
            {
                _hasVelocitySet = true;
                velocity = new Vector3(-movingSpeed, 0, 0);
            }
        }

        /// <summary>
        /// 接触時にダメージを与える
        /// </summary>
        public void DealCollisionDamage()
        {
            _playerStatusController.ChangeHP(
                DamageFactorData.damageFactorData.collisionDamage[factorType]
                );

            // 自爆型の敵の場合は消滅する
            if (factorType == DamageFactorData.damageFactorType.NormalEnemy__SelfDestruct)
            {
                isBeingDestroyed = true;
            }
        }
    }
}
