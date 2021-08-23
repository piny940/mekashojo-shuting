using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PauseController
    {
        private bool _isFirstTime = true;
        private bool _isPauseScreenVisible = false;
        private float _startTimeCounter = 0;

        public UnityEvent<bool> OnPauseScreenVisibilityChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnStartTimeCounterChanged = new UnityEvent<float>();

        public bool isPauseScreenVisible
        {
            get { return _isPauseScreenVisible; }
            set
            {
                _isPauseScreenVisible = value;
                OnPauseScreenVisibilityChanged?.Invoke(_isPauseScreenVisible);
            }
        }

        public float startTimeCounter
        {
            get { return _startTimeCounter; }
            set
            {
                _startTimeCounter = value;
                OnStartTimeCounterChanged?.Invoke(_startTimeCounter);
            }
        }

        public bool isGameGoing = false;

        public void RunEveryFrame()
        {
            CheckPausing();
            StartCount();
        }


        private void CheckPausing()
        {
            //escキーを押したら停止する
            if (InputController.isEscapeKey && isGameGoing)
            {
                isGameGoing = false;
                isPauseScreenVisible = true;
            }
        }

        private void StartCount()
        {
            //カウントダウンをする
            if (!isGameGoing && !isPauseScreenVisible || _isFirstTime)
            {
                startTimeCounter += Time.deltaTime;

                if (startTimeCounter > 3)
                {
                    _isFirstTime = false;

                    startTimeCounter = 0;

                    isGameGoing = true;
                }
            }
        }
    }
}
