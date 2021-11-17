using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class StageStatusManager
    {
        private const float COUNT_DOWN_TIME = 3;
        private float _startTimeCounter = 0;
        private stageStatus _secondPreviousStageStatus = stageStatus.Normal;
        private stageStatus _previousStageStatus = stageStatus.Normal;
        private stageStatus _currentStageStatus = stageStatus.CountDown;

        public UnityEvent<float> OnStartTimeCounterChanged = new UnityEvent<float>();
        public UnityEvent<stageStatus> OnCurrentStageStatusChanged = new UnityEvent<stageStatus>();

        public float startTimeCounter
        {
            get { return _startTimeCounter; }
            set
            {
                _startTimeCounter = value;
                OnStartTimeCounterChanged?.Invoke(value);
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
            BossDead,
            PlayerDying,
            IsPausing, // IsPausingはポーズ画面が表示されていることと同値
            CountDown,
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
                ChangeStatus(stageStatus.IsPausing);
            }
        }

        private void StartCount()
        {
            //カウントダウンをする
            if (currentStageStatus == stageStatus.CountDown)
            {
                startTimeCounter += Time.deltaTime;

                if (startTimeCounter > COUNT_DOWN_TIME)
                {
                    startTimeCounter = 0;

                    ChangeStatus(_secondPreviousStageStatus);

                    isGameGoing = true;
                }
            }
        }

        // ステージの状態を変えるときに呼ぶ
        public void ChangeStatus(stageStatus status)
        {
            _secondPreviousStageStatus = _previousStageStatus;
            _previousStageStatus = currentStageStatus;
            currentStageStatus = status;
        }

        // Restartボタンを押したときに呼ばれる
        public void Restart()
        {
            ChangeStatus(stageStatus.CountDown);
        }
    }
}
