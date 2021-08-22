using UnityEngine;
using UnityEngine.Events;

namespace Model
{
    public class Bomb__Player
    {
        private const float MAX_BOBM_SIZE = 4.5f;
        private const float BOMB_EXPAND_SPEED = 3;

        private PlayerStatusController _playerStatusController;
        private PauseController _pauseController;
        private bool _isBombActive = false;
        private float _bombSize = 0;
        private bool _isUsingBomb = false;

        public UnityEvent<bool> OnIsBombActiveChanged = new UnityEvent<bool>();
        public UnityEvent<float> OnBombSizeChanged = new UnityEvent<float>();

        public bool isBombActive
        {
            get { return _isBombActive; }
            set
            {
                _isBombActive = value;
                OnIsBombActiveChanged?.Invoke(value);
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

        public Bomb__Player(PlayerStatusController playerStatusController, PauseController pauseController)
        {
            _playerStatusController = playerStatusController;
            _pauseController = pauseController;
        }

        public void ProceedBomb()
        {
            if (!_pauseController.isGameGoing) return;

            // ボムを発射するキーが押されていて、かつボムを所持していたら、「ボムを使用中」にする
            if (InputController.bombKey > 0
                && _playerStatusController.bombAmount != 0
                && !_isUsingBomb)
            {
                _isUsingBomb = true;
                isBombActive = true;
                _playerStatusController.bombAmount--;
            }

            if (!_isUsingBomb) return;

            if (bombSize < MAX_BOBM_SIZE)
            {
                //ボムを大きくしていく
                bombSize += BOMB_EXPAND_SPEED * Time.deltaTime;
            }
            else
            {
                bombSize = 0;
                isBombActive = false;
                _isUsingBomb = false;
            }
        }
    }
}
