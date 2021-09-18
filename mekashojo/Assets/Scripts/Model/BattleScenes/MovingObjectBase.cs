using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public abstract class MovingObjectBase
    {
        private const float SCREEN_FRAME = 1;
        private Vector3 _savedVelocity;
        private Vector3 _velocity;
        private bool _isMoving = true;
        private bool _isBeingDestroyed = false;
        private float _time = 0;
        private EnemyManager _enemyManager;

        protected abstract movingObjectType objectType { get; set; }
        protected PauseManager pauseManager;
        protected float disappearTime = 0;

        public UnityEvent<Vector3> OnVelocityChanged = new UnityEvent<Vector3>();
        public UnityEvent<bool> OnIsMovingChanged = new UnityEvent<bool>();
        public UnityEvent<bool> OnIsBeingDestroyedChanged = new UnityEvent<bool>();

        public enum movingObjectType
        {
            Player,
            PlayerFire,
            Enemy,
            EnemyFire,
            DropItem,
        }

        public Vector3 velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
                OnVelocityChanged?.Invoke(_velocity);
            }
        }

        public bool isMoving
        {
            get { return _isMoving; }
            set
            {
                _isMoving = value;
                OnIsMovingChanged?.Invoke(value);
            }
        }

        public bool isBeingDestroyed
        {
            get { return _isBeingDestroyed; }
            set
            {
                if (objectType == movingObjectType.Enemy)
                {
                    _enemyManager.totalEnemyAmount--;
                }

                _isBeingDestroyed = value;
                OnIsBeingDestroyedChanged?.Invoke(_isBeingDestroyed);
            }
        }

        public MovingObjectBase(EnemyManager enemyManager, PauseManager pauseManager)
        {
            _enemyManager = enemyManager;
            this.pauseManager = pauseManager;
        }

        /// <summary>
        /// ポーズ時に停止する
        /// </summary>
        protected void StopOnPausing()
        {
            //ポーズし始めた時
            if (!pauseManager.isGameGoing && isMoving)
            {
                //速度の保存
                _savedVelocity = velocity;

                //停止
                velocity = new Vector3(0, 0, 0);

                isMoving = false;

                return;
            }

            //ポーズし終わった時
            if (pauseManager.isGameGoing && !isMoving)
            {
                isMoving = true;

                velocity = _savedVelocity;
            }
        }

        /// <summary>
        /// 画面の外に出たら消滅する
        /// </summary>
        protected void DisappearIfOutside(Vector3 thisPosition)
        {
            //画面左下と右上の座標の取得
            Vector3 cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            //画面の外に出たら消滅する
            //画面外という判定にはSCREEN_FRAMEの分だけ余裕を持たせておく
            if (thisPosition.x < cornerPosition__LeftBottom.x - SCREEN_FRAME
                || thisPosition.x > cornerPosition__RightTop.x + SCREEN_FRAME
                || thisPosition.y > cornerPosition__RightTop.y + SCREEN_FRAME
                || thisPosition.y < cornerPosition__LeftBottom.y - SCREEN_FRAME)
            {
                isBeingDestroyed = true;
            }
        }

        /// <summary>
        /// 一定時間経過後消滅する
        /// このメソッドを呼ぶ場合は、コンストラクタでdisappearTimeの値を設定する必要がある
        /// </summary>
        protected void DisappearLater()
        {
            // disappearTimeの設定がされていなければ、このメソッドでは何もしない
            if (disappearTime == 0) return;

            _time += Time.deltaTime;

            if (_time > disappearTime)
            {
                isBeingDestroyed = true;
            }
        }
    }
}
