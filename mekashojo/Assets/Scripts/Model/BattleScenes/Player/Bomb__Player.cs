using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Bomb__Player
    {
        private const float MAX_BOMB_SIZE = 4.5f;
        private const float BOMB_EXPAND_SPEED = 3;

        private PlayerStatusManager _playerStatusManager;
        private PlayerDebuffManager _playerDebuffManager;
        private PauseManager _pauseManager;
        private bool _isUsingBomb = false;
        private float _bombSize = 0;

        public UnityEvent<bool> OnIsUsingBombChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnBombSizeChanged = new UnityEvent<float>();

        public bool isUsingBomb
        {
            get { return _isUsingBomb; }
            set
            {
                _isUsingBomb = value;
                OnIsUsingBombChanged?.Invoke(value);
            }
        }

        public float bombSize
        {
            get { return _bombSize; }
            set
            {
                _bombSize = value;
                OnBombSizeChanged?.Invoke(value);
            }
        }

        public Bomb__Player(PlayerDebuffManager playerDebuffManager, PlayerStatusManager playerStatusManager, PauseManager pauseManager)
        {
            _playerDebuffManager = playerDebuffManager;
            _playerStatusManager = playerStatusManager;
            _pauseManager = pauseManager;
        }

        public void RunEveryFrame()
        {
            ProceedBomb();
        }

        private void ProceedBomb()
        {
            if (!_pauseManager.isGameGoing) return;

            // ボムを発射するキーが押されていて、かつボムを所持していたら、「ボムを使用中」にする
            // スタンしているときはボムを使用できない
            if (InputManager.bombKey > 0
                && _playerStatusManager.bombAmount != 0
                && !_playerDebuffManager.isStunned
                && !isUsingBomb)
            {
                isUsingBomb = true;
                _playerStatusManager.bombAmount--;
            }

            if (!isUsingBomb) return;

            if (bombSize < MAX_BOMB_SIZE)
            {
                //ボムを大きくしていく
                bombSize += BOMB_EXPAND_SPEED * Time.deltaTime;
            }
            else
            {
                bombSize = 0;
                isUsingBomb = false;
            }
        }
    }
}
