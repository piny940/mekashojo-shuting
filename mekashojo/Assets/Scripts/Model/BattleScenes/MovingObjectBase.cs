using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public abstract class MovingObjectBase
    {
        private const float SCREEN_FRAME = 1;
        private Vector3 _savedVelocity;
        private Vector3 _velocity;
        private bool _isFirstTime = true;
        private bool _isMoving = false;
        private bool _isBeingDestroyed = false;
        private EnemyController _enemyController;

        protected abstract movingObjectType objectType { get; set; }
        protected PauseController pauseController;

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
                _isBeingDestroyed = value;
                OnIsBeingDestroyedChanged?.Invoke(_isBeingDestroyed);
            }
        }

        public MovingObjectBase(EnemyController enemyController, PauseController pauseController)
        {
            _enemyController = enemyController;
            this.pauseController = pauseController;
        }

        /// <summary>
        /// ポーズ時に停止する
        /// </summary>
        protected void StopOnPausing()
        {
            //ポーズし始めた時
            if (!pauseController.isGameGoing && isMoving)
            {
                //速度の保存
                _savedVelocity = velocity;

                //停止
                velocity = new Vector3(0, 0, 0);

                isMoving = false;

                return;
            }

            //ポーズし終わった時
            if (pauseController.isGameGoing && !isMoving)
            {
                isMoving = true;

                if (_isFirstTime)
                {
                    //ゲーム開始時は速度はセットしない
                    _isFirstTime = false;
                    return;
                }

                velocity = _savedVelocity;
            }
        }

        /// <summary>
        /// 画面の外に出たら消滅する
        /// </summary>
        protected void DestroyLater(Vector3 thisPosition)
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

                if (objectType == movingObjectType.Enemy)
                {
                    _enemyController.totalEnemyAmount--;
                }
            }
        }
    }
}
