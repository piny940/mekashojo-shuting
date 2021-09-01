using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class PauseManager
    {
        private bool _isFirstTime = true;
        private bool _isPausing = false;
        private float _startTimeCounter = 0;

        public UnityEvent<float> OnStartTimeCounterChanged = new UnityEvent<float>();
        public UnityEvent<bool> OnIsPausingChanged = new UnityEvent<bool>();

        public float startTimeCounter
        {
            get { return _startTimeCounter; }
            set
            {
                _startTimeCounter = value;
                OnStartTimeCounterChanged?.Invoke(_startTimeCounter);
            }
        }

        // isPausingはポーズ画面が表示されていることと同値
        // escapeキーが押されたらtrueになり、Returnボタンが押されたらfalseになる
        public bool isPausing
        {
            get { return _isPausing; }
            set
            {
                _isPausing = value;
                OnIsPausingChanged?.Invoke(_isPausing);
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
            if (InputManager.isEscapeKey && isGameGoing)
            {
                isGameGoing = false;
                isPausing = true;
            }
        }

        private void StartCount()
        {
            //カウントダウンをする
            if (!isGameGoing && !isPausing || _isFirstTime)
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
