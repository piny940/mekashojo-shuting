using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class StageStatusManager
    {
        private const float COUNT_DOWN_TIME = 3;
        private bool _isFirstTime = true;
        private float _startTimeCounter = 0;
        private stageStatus _previousStageStatus = stageStatus.Normal;
        private stageStatus _currentStageStatus = stageStatus.Normal;

        public UnityEvent<float> OnStartTimeCounterChanged = new UnityEvent<float>();
        public UnityEvent<stageStatus> OnCurrentStageStatusChanged = new UnityEvent<stageStatus>();

        public float startTimeCounter
        {
            get { return _startTimeCounter; }
            set
            {
                _startTimeCounter = value;
                OnStartTimeCounterChanged?.Invoke(_startTimeCounter);
            }
        }

        public stageStatus currentStageStatus
        {
            get { return _currentStageStatus; }
            private set
            {
                _currentStageStatus = value;
                OnCurrentStageStatusChanged?.Invoke(value);
            }
        }

        public bool isGameGoing = false;

        public enum stageStatus
        {
            Normal,
            BossAppearing,
            BossBattle,
            BossDying,
            PlayerDying,
            IsPausing, // IsPausingはポーズ画面が表示されていることと同値
        }

        public void RunEveryFrame()
        {
            CheckPausing();
            StartCount();
        }

        private void CheckPausing()
        {
            // escキーを押したら停止する
            // ボス出現演出中はポーズできない
            if (InputManager.isEscapeKey
                && isGameGoing
                && currentStageStatus != stageStatus.BossAppearing)
            {
                isGameGoing = false;
                currentStageStatus = stageStatus.IsPausing;
            }
        }

        private void StartCount()
        {
            //カウントダウンをする
            if ((!isGameGoing && currentStageStatus != stageStatus.IsPausing)
                || _isFirstTime)
            {
                startTimeCounter += Time.deltaTime;

                if (startTimeCounter > COUNT_DOWN_TIME)
                {
                    _isFirstTime = false;

                    startTimeCounter = 0;

                    isGameGoing = true;
                }
            }
        }

        // ステージの状態を変えるときに呼ぶ
        public void ChangeStatus(stageStatus status)
        {
            _previousStageStatus = currentStageStatus;
            currentStageStatus = status;
        }

        // Restartボタンを押したときに呼ばれる
        public void Restart()
        {
            // currentStageStatusと_previousStageStatusのswap
            stageStatus tmp = currentStageStatus;
            currentStageStatus = _previousStageStatus;
            _previousStageStatus = tmp;
        }
    }
}
