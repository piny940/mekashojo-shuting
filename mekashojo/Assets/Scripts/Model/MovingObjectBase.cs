using UnityEngine;
using UnityEngine.Events;
using System;

namespace Model
{
    public class MovingObjectBase
    {
        private bool _hasStopped = true;
        private Vector3 _savedVelocity;
        private bool _isFirstTime = true;
        private const float SCREEN_FRAME = 1;
        private Vector3 _velocity;
        private bool _isDestroyed = false;
        private bool _isAnimationPlaying = false;

        protected PauseController pauseController;

        public UnityEvent<bool> OnIsAnimationPlayingChanged = new UnityEvent<bool>();
        public UnityEvent<Vector3> OnVelocityChanged = new UnityEvent<Vector3>();
        public UnityEvent<bool> OnIsDestroyedChanged = new UnityEvent<bool>();

        public bool isAnimationPlaying
        {
            get { return _isAnimationPlaying; }
            set
            {
                _isAnimationPlaying = value;
                OnIsAnimationPlayingChanged?.Invoke(_isAnimationPlaying);
            }
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

        public bool isDestroyed
        {
            get { return _isDestroyed; }
            set
            {
                _isDestroyed = value;
                OnIsDestroyedChanged?.Invoke(_isDestroyed);
            }
        }

        public MovingObjectBase(PauseController pauseController)
        {
            this.pauseController = pauseController;
        }

        /// <summary>
        /// ポーズ時に停止する
        /// </summary>
        public void StopOnPausing()
        {
            //ポーズし始めた時
            if (!pauseController.isGameGoing && !_hasStopped)
            {
                //速度の保存
                _savedVelocity = velocity;

                //停止
                velocity = new Vector3(0, 0, 0);

                _hasStopped = true;

                isAnimationPlaying = false;

                return;
            }

            //ポーズし終わった時
            if (pauseController.isGameGoing && _hasStopped)
            {
                _hasStopped = false;

                isAnimationPlaying = true;

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
        public void DestroyLater(Vector3 thisPosition)
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
                isDestroyed = true;
            }
        }
    }
}
