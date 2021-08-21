using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Model
{
    public class EnemyManager : MovingObjectBase
    {
        private bool _hasVelocitySet = false;
        private bool _hasAnimationStarted = false;

        private int _frameCount = 0;
        private int _firingCount = 0;

        private float _firingTime = 0;
        private bool _isFiring = false;
        private bool _isNoticingFire = false;

        private const float CONTACT_DAMAGE_AMOUNT = 10;

        private beamFiringProcesses _beamStatus = beamFiringProcesses.HasStoppedBeam;

        public Action<Vector3, string> FireBullet;


        public UnityEvent<beamFiringProcesses> OnBeamStatusChanged = new UnityEvent<beamFiringProcesses>();

        public beamFiringProcesses beamStatus
        {
            get { return _beamStatus; }
            set
            {
                _beamStatus = value;
                OnBeamStatusChanged?.Invoke(_beamStatus);
            }
        }


        public enum beamFiringProcesses
        {
            IsNoticingBeamFiring,
            IsFiringBeam,
            HasStoppedBeam,
        }


        public struct FiringBulletSettings
        {
            public float shortFiringIntervalFrameAmount;
            public List<Vector3> bulletVelocities;
            public string firePath;
            public int firingAmount;
        }


        protected EnemyController enemyController;
        private PlayerStatusController _playerStatusController;

        protected EnemyManager(PauseController pauseController, EnemyController enemyController, PlayerStatusController playerStatusController) : base(pauseController)
        {
            _playerStatusController = playerStatusController;

            //敵オブジェクトの場合は消滅するときに敵の数を1減らす処理をする
            //ModelクラスにAddListenerを書くのが最高に気持ち悪いので改善案求む
            OnIsDestroyedChanged.AddListener((bool isDestroyed) =>
            {
                if (isDestroyed) { enemyController.totalEnemyAmount--; }
            });
        }

        /// <summary>
        /// 弾を発射するタイプの敵はこのメソッドを呼ぶ
        /// 攻撃中かどうかを戻り値で返すようにしたんやけど、
        /// このメソッド、使い方が特殊すぎて属人性が高そう。改善案求む。
        /// </summary>
        protected bool IsBulletsProcessRunning(FiringBulletSettings firingBulletSettings)
        {
            _frameCount++;

            //一定小時間ごとに発射する
            if (_frameCount > firingBulletSettings.shortFiringIntervalFrameAmount)
            {
                _frameCount = 0;

                foreach (Vector3 bulletVelocity in firingBulletSettings.bulletVelocities)
                {
                    // TODO:AddListenerの使い方間違ってる！修正！
                    FireBullet?.Invoke(bulletVelocity, "EnemyFire__" + firingBulletSettings.firePath);
                }

                _firingCount++;
            }

            //一定数発射したら発射を終了する
            if (_firingCount >= firingBulletSettings.firingAmount)
            {
                _firingCount = 0;
                return false;
            }

            return true;
        }

        /// <summary>
        /// ビームを発射する敵はこのメソッドを呼ぶ
        /// 攻撃中かどうかを戻り値で返すようにしたんやけど、
        /// このメソッド、使い方が特殊すぎて属人性が高そう。改善案求む。
        /// </summary>
        protected bool IsBeamsProcessRunning(float beamNoticingTime, float beamTime)
        {
            _firingTime += Time.deltaTime;

            //攻撃の予告
            if (_firingTime <= beamNoticingTime && !_isNoticingFire)
            {
                beamStatus = beamFiringProcesses.IsNoticingBeamFiring;

                _isNoticingFire = true;
            }

            //攻撃時
            if (_firingTime > beamNoticingTime && _firingTime <= beamNoticingTime + beamTime && !_isFiring)
            {
                _isNoticingFire = false;

                beamStatus = beamFiringProcesses.IsFiringBeam;

                _isFiring = true;
            }

            //攻撃終了時
            if (_firingTime > beamNoticingTime + beamTime)
            {
                beamStatus = beamFiringProcesses.HasStoppedBeam;

                _isFiring = false;
                _firingTime = 0;

                return false;
            }

            return true;
        }


        /// <summary>
        /// 移動速度の設定（移動速度が一定の場合)
        /// </summary>
        protected void SetConstantVelocity(float movingSpeed)
        {
            //まだ始まってなかったら抜ける
            if (!pauseController.isGameGoing)
            {
                return;
            }

            if (!_hasVelocitySet)
            {
                _hasVelocitySet = true;
                velocity = new Vector3(-movingSpeed, 0, 0);

            }
        }


        /// <summary>
        /// アニメーションをスタートする<br></br>
        /// Updateで呼ぶ
        /// </summary>
        protected void StartAnimation()
        {
            //まだ始まってなかったら抜ける
            if (!pauseController.isGameGoing)
            {
                return;
            }

            if (!_hasAnimationStarted)
            {
                isAnimationPlaying = true;
                _hasAnimationStarted = true;
            }
        }

        public void DoDamageBase(float damageAmount)
        {
            _playerStatusController.ChangeHP(damageAmount);
        }

        public void DoDamage()
        {
            DoDamageBase(CONTACT_DAMAGE_AMOUNT);
        }
    }
}
