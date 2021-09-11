using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Enemy__Boss1 : DamageFactorManager
    {
        private const float FIRING_INTERVAL = 5;

        // ビーム用の定数
        private const float BEAM_NOTIFYING_TIME = 1;
        private const float BEAM_TIME = 5;

        // ミサイル用の定数
        private const int MISSILE_SHORT_INTERVAL = 0; // 一発しか撃たないから0でいい
        private const int MISSILE_FIRING_AMOUNT = 1;
        private const float MISSILE_SPEED = 5;

        // 拡散弾用の定数
        private const int SPREAD_BULLET_SHORT_INTERVAL = 20;
        private const int SPREAD_BULLET_FIRING_AMOUNT = 15; // 攻撃が仕様書通り5秒間続くよう調整
        private const int SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE = 15;
        private const float SPREAD_BULLET_SPREAD = 12;

        // 広範囲ビーム用の定数
        private const float WIDE_BEAM_NOTIFYING_TIME = 1;
        private const float WIDE_BEAM_TIME = 5;

        // 誘導弾用の定数
        private const int GUIDED_BULLET_SHORT_INTERVAL = 20;
        private const int GUIDED_BULLET_FIRING_AMOUNT = 45; // 攻撃が仕様書通り15秒間続くよう調整
        private const int GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE = 10;
        private const float GUIDED_BULLET_SPREAD = 3;

        // 拡散ビーム用の定数
        private const float SPREAD_BEAM_NOTIFYING_TIME = 1;
        private const float SPREAD_BEAM_TIME = 5;

        // 各種類の攻撃をする可能性の比
        private readonly Dictionary<attackType, float> _attackProbabilityRatios
            = new Dictionary<attackType, float>()
            {
                { attackType.Beam, 1 },
                { attackType.Missile, 1 },
                { attackType.SpreadBullet, 1 },
                { attackType.WideBeam, 1 },
                { attackType.GuidedBullet, 1 },
                { attackType.SpreadBeam, 1 },
            };

        private attackType _proceedingAttackTypeName = attackType._none; // 今どの攻撃をしているか
        private float _time = 0;
        private float _attackingFrameCount = 0;

        private BulletProcessInfo _missileProcessInfo;
        private BulletProcessInfo _spreadBulletProcessInfo;
        private BulletProcessInfo _guidedBulletProcessInfo;

        private ObservableCollection<object> _firingMissileInfo;
        private ObservableCollection<object> _firingNormalBulletInfo;
        private ObservableCollection<object> _firingGuidedBulletInfo;

        public UnityEvent<ObservableCollection<object>> OnFiringMissileInfoChanged
            = new UnityEvent<ObservableCollection<object>>();

        public UnityEvent<ObservableCollection<object>> OnFiringNormalBulletInfoChanged
            = new UnityEvent<ObservableCollection<object>>();

        public UnityEvent<ObservableCollection<object>> OnFiringGuidedBulletInfoChanged
            = new UnityEvent<ObservableCollection<object>>();

        public ObservableCollection<object> firingMissileInfo
        {
            get { return _firingMissileInfo; }
            set
            {
                _firingMissileInfo = value;
                OnFiringMissileInfoChanged?.Invoke(value);
            }
        }

        public ObservableCollection<object> firingNormalBulletInfo
        {
            get { return _firingNormalBulletInfo; }
            set
            {
                _firingNormalBulletInfo = value;
                OnFiringNormalBulletInfoChanged?.Invoke(value);
            }
        }

        public ObservableCollection<object> firingGuidedBulletInfo
        {
            get { return _firingGuidedBulletInfo; }
            set
            {
                _firingGuidedBulletInfo = value;
                OnFiringGuidedBulletInfoChanged?.Invoke(value);
            }
        }

        // ステージ1のボスがする攻撃の種類
        private enum attackType
        {
            _none,
            Beam,
            Missile,
            SpreadBullet,
            WideBeam,
            GuidedBullet,
            SpreadBeam,
        }

        protected override DamageFactorData.damageFactorType factorType { get; set; }

        public Enemy__Boss1(PauseManager pauseManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(pauseManager, enemyManager, playerStatusManager)
        {
            factorType = DamageFactorData.damageFactorType.Boss;

            // ミサイル発射の設定
            // 発射速度は攻撃時に設定する
            _missileProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = MISSILE_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.Missile.ToString(),
                firingAmount = MISSILE_FIRING_AMOUNT,
            };

            // 拡散弾発射の設定
            _spreadBulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = SPREAD_BULLET_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.SpreadBullet.ToString(),
                firingAmount = SPREAD_BULLET_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _spreadBulletProcessInfo.bulletVelocities.Add(
                    SPREAD_BULLET_SPREAD
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / SPREAD_BULLET_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }

            // 誘導弾の設定
            _guidedBulletProcessInfo = new BulletProcessInfo()
            {
                shortInterval_Frame = GUIDED_BULLET_SHORT_INTERVAL,
                firePath = "Boss1__" + attackType.GuidedBullet.ToString(),
                firingAmount = GUIDED_BULLET_FIRING_AMOUNT,
                bulletVelocities = new List<Vector3>(),
            };

            for (int i = 0; i < GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE; i++)
            {
                //弾を発射する方向を計算
                _guidedBulletProcessInfo.bulletVelocities.Add(
                    GUIDED_BULLET_SPREAD
                    * new Vector3(
                        Mathf.Cos(Mathf.PI / 2 + Mathf.PI * i / GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE),
                        Mathf.Sin(Mathf.PI / 2 + Mathf.PI * i / GUIDED_BULLET_FIRING_AMOUNT_PER_ONCE),
                        0)
                    );
            }
        }

        public void RunEveryFrame(Vector3 position, Vector3 playerPosition)
        {
            StopOnPausing();
            ProceedAttack(position, playerPosition);
        }

        protected override void FireBullet(ObservableCollection<object> firingBulletInfo)
        {
            switch (_proceedingAttackTypeName)
            {
                case attackType.Missile:
                    firingMissileInfo = firingBulletInfo;
                    break;

                case attackType.SpreadBullet:
                    firingNormalBulletInfo = firingBulletInfo;
                    break;

                case attackType.GuidedBullet:
                    firingGuidedBulletInfo = firingBulletInfo;
                    break;
            }
        }

        private void ProceedAttack(Vector3 position, Vector3 playerPosition)
        {
            _time += Time.deltaTime;

            // 攻撃をやめる処理
            if (_proceedingAttackTypeName == attackType._none && _attackingFrameCount > 0)
            {
                _attackingFrameCount = 0;
                return;
            }

            // 攻撃を始める処理
            if (_time > FIRING_INTERVAL && _proceedingAttackTypeName == attackType._none)
            {
                _time = 0;
                _proceedingAttackTypeName = RandomChoosing.ChooseRandomly(_attackProbabilityRatios);

                // 攻撃開始時に処理が必要なのはミサイルだけなのでif文で済ませておく
                if (_proceedingAttackTypeName == attackType.Missile)
                {
                    StartMissile(position, playerPosition);
                }
            }

            // 攻撃中でない場合はここで終了
            if (_proceedingAttackTypeName == attackType._none) return;

            _attackingFrameCount++;

            // 攻撃本体
            switch (_proceedingAttackTypeName)
            {
                case attackType.Beam:
                    ProceedBeam();
                    break;

                case attackType.Missile:
                    ProceedMissile();
                    break;

                case attackType.SpreadBullet:
                    ProceedSpreadBullet();
                    break;

                case attackType.WideBeam:
                    ProceedWideBeam();
                    break;

                case attackType.GuidedBullet:
                    ProceedGuidedBullet();
                    break;

                case attackType.SpreadBeam:
                    ProceedSpreadBeam();
                    break;
            }
        }

        // ビーム攻撃の処理はどれもほぼ同じなので、まとめられるところはまとめてしまう
        private void ProceedBeamBase(float beamNotifyingTime, float beamTime)
        {
            bool isAttacking = ProceedBeamFiring(beamNotifyingTime, beamTime);
            if (!isAttacking) _proceedingAttackTypeName = attackType._none;

            // ProceedBeamFiringは本来一定時間が経てば自動的にfalseを返すようになるのだが、
            // 何らかの原因でfalseを返さなくなった場合を想定して、一定時間が経過したら
            // 強制的に攻撃を終了するプログラムを書いておく(以下同)
            if (_attackingFrameCount * Time.deltaTime > beamNotifyingTime
                                    + beamTime
                                    + EXTRA_FRAME_AMOUNT)
            {
                _proceedingAttackTypeName = attackType._none;
                ResetAttacking();
            }
        }

        // ビーム攻撃の処理
        private void ProceedBeam()
        {
            ProceedBeamBase(BEAM_NOTIFYING_TIME, BEAM_TIME);
        }

        // ミサイル攻撃を始める処理
        private void StartMissile(Vector3 position, Vector3 playerPosition)
        {
            Vector3 newPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, EnemyManager.enemyPosition__z);

            // 発射方向をプレイヤがいる向きに設定
            _missileProcessInfo.bulletVelocities
                = new List<Vector3>()
                { (newPlayerPosition - position) * MISSILE_SPEED / Vector3.Magnitude(newPlayerPosition - position) };

            _attackingFrameCount++;
        }

        // ミサイル攻撃の処理
        private void ProceedMissile()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_missileProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking)
            {
                _proceedingAttackTypeName = attackType._none;
                return;
            }

            if (_attackingFrameCount > MISSILE_FIRING_AMOUNT
                                            * MISSILE_SHORT_INTERVAL
                                            + EXTRA_FRAME_AMOUNT)
            {
                _proceedingAttackTypeName = attackType._none;
                ResetAttacking();
            }
        }

        // 拡散弾の攻撃
        private void ProceedSpreadBullet()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_spreadBulletProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking)
            {
                _proceedingAttackTypeName = attackType._none;
                return;
            }

            if (_attackingFrameCount > SPREAD_BULLET_FIRING_AMOUNT
                                            * SPREAD_BULLET_SHORT_INTERVAL
                                            + EXTRA_FRAME_AMOUNT)
            {
                _proceedingAttackTypeName = attackType._none;
                ResetAttacking();
            }
        }

        // 広範囲ビームの処理
        private void ProceedWideBeam()
        {
            ProceedBeamBase(WIDE_BEAM_NOTIFYING_TIME, WIDE_BEAM_TIME);
        }

        // 誘導弾の処理
        private void ProceedGuidedBullet()
        {
            // 攻撃本体
            bool isAttacking = ProceedBulletFiring(_guidedBulletProcessInfo);

            // 攻撃終了時の処理
            if (!isAttacking)
            {
                _proceedingAttackTypeName = attackType._none;
                return;
            }

            if (_attackingFrameCount > GUIDED_BULLET_FIRING_AMOUNT
                                            * GUIDED_BULLET_SHORT_INTERVAL
                                            + EXTRA_FRAME_AMOUNT)
            {
                _proceedingAttackTypeName = attackType._none;
                ResetAttacking();
            }
        }

        // 拡散ビームの処理
        private void ProceedSpreadBeam()
        {
            ProceedBeamBase(SPREAD_BEAM_NOTIFYING_TIME, SPREAD_BEAM_TIME);
        }
    }
}
