using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    // ObservableCollection<object>とFiringBulletInfoの変換をこのクラスのメソッドで行う
    public static class FiringInfoConverter
    {
        // 引数で与えられた値をObservableCollectionにまとめて返す
        public static ObservableCollection<object> MakeCollection(Vector3 vector3, string str)
        {
            return
                new ObservableCollection<object>()
                {
                    vector3,
                    str,
                };
        }

        // 引数で与えられたObservableCollectionから弾の速度、
        // 弾のPrefabのパスを取り出して構造体として返す
        public static DamageFactorManager.FiringBulletInfo MakeStruct(ObservableCollection<object> collection)
        {
            return
                new DamageFactorManager.FiringBulletInfo()
                {
                    bulletVelocity = (Vector3)collection[0],
                    firePath = (string)collection[1],
                };
        }
    }

    // 雑魚敵、ステージボス、触れたらダメージを受ける壁など、プレイヤーにダメージを
    // 与えるオブジェクトのクラスはこのクラスを継承する
    public abstract class DamageFactorManager : MovingObjectBase
    {
        protected const int EXTRA_FRAME_AMOUNT = 3; //なんか知らんけどこの値は3以上じゃないとうまくいかんかった
        private bool _hasVelocitySet = false;
        private int _frameCount = 0;
        private int _firingCount = 0;
        private float _firingTime = 0;
        private float _producingEnemyTime = 0;
        private float _producingEnemyCount = 0;
        private beamFiringProcesses __beamStatus;
        private beamFiringProcesses _beamStatus
        {
            get { return __beamStatus; }
            set
            {
                __beamStatus = value;
                ChangeBeamStatus(value);
            }
        }

        protected readonly PlayerStatusManager playerStatusManager;
        private ObservableCollection<object> _firingBulletInfo;
        protected override movingObjectType objectType { get; set; }
        protected abstract DamageFactorData.damageFactorType factorType { get; set; }

        // ステージボスでは広範囲ビームと拡散ビームの2通りがあって、その切り替え処理を行わないと
        // いけないため、実装は子クラスで行う
        protected abstract void ChangeBeamStatus(beamFiringProcesses process);

        public UnityEvent<ObservableCollection<object>> OnFiringBulletInfoChanged
            = new UnityEvent<ObservableCollection<object>>();

        public ObservableCollection<object> firingBulletInfo
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
            public int shortInterval_Frame;
            public List<Vector3> bulletVelocities;
            public string firePath;
            public int firingAmount;
        }

        protected DamageFactorManager(PauseManager pauseManager, EnemyManager enemyManager, PlayerStatusManager playerStatusManager)
                : base(enemyManager, pauseManager)
        {
            this.playerStatusManager = playerStatusManager;
            objectType = movingObjectType.Enemy;
        }

        /// <summary>
        /// 弾を発射するタイプの敵はこのメソッドを呼ぶ
        /// </summary>
        protected bool ProceedBulletFiring(BulletProcessInfo bulletProcessInfo)
        {
            //一定数発射したら発射を終了する
            if (_firingCount >= bulletProcessInfo.firingAmount)
            {
                _firingCount = 0;
                return false;
            }

            _frameCount++;

            //一定小時間ごとに発射する
            if (_frameCount > bulletProcessInfo.shortInterval_Frame)
            {
                _frameCount = 0;

                foreach (Vector3 bulletVelocity in bulletProcessInfo.bulletVelocities)
                {
                    firingBulletInfo
                        = FiringInfoConverter.MakeCollection(
                            bulletVelocity,
                            "Enemy/EnemyFire__" + bulletProcessInfo.firePath
                            );
                }

                _firingCount++;
            }

            return true;
        }

        /// <summary>
        /// ビームを発射する敵はこのメソッドを呼ぶ
        /// </summary>
        protected bool ProceedBeamFiring(float beamNotifyingTime, float beamTime)
        {
            _firingTime += Time.deltaTime;

            //攻撃の予告
            if (_firingTime <= beamNotifyingTime && _beamStatus != beamFiringProcesses.IsNotifyingBeamFiring)
            {
                _beamStatus = beamFiringProcesses.IsNotifyingBeamFiring;
            }

            //攻撃時
            if (_firingTime > beamNotifyingTime && _firingTime <= beamNotifyingTime + beamTime && _beamStatus != beamFiringProcesses.IsFiringBeam)
            {
                _beamStatus = beamFiringProcesses.IsFiringBeam;
            }

            //攻撃終了時
            if (_firingTime > beamNotifyingTime + beamTime)
            {
                _beamStatus = beamFiringProcesses.HasStoppedBeam;

                _firingTime = 0;

                return false;
            }

            return true;
        }

        // 敵を生成する間隔と敵の生成比の辞書を引数で与え、敵を生成する処理を行う
        protected bool ProceedEnemyCreating(float shortInterval__Time, float amount, Dictionary<Controller.NormalEnemyData.normalEnemyType, float> probabilityRatios)
        {
            // 攻撃終了時
            if (_producingEnemyCount > amount)
            {
                _producingEnemyCount = 0;
                return false;
            }

            _producingEnemyTime += Time.deltaTime;

            if (_producingEnemyTime > shortInterval__Time)
            {
                _producingEnemyTime = 0;
                _producingEnemyCount++;
                enemyManager.CreateNormalEnemy(RandomChoosing.ChooseRandomly(probabilityRatios));
            }

            return true;
        }

        /// <summary>
        /// 移動速度の設定(移動速度が一定の場合)
        /// </summary>
        protected void SetConstantVelocity(float movingSpeed)
        {
            //まだ始まってなかったら抜ける
            if (!pauseManager.isGameGoing) return;

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
            playerStatusManager.GetDamage(
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
