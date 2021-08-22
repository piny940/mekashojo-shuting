using UnityEngine;

namespace Model
{
    public class PlayerPositionController : MovingObjectBase
    {
        private int _stunFrameCount = 0;
        private Vector3 _shakingVector = Vector3.zero;
        private float _stunTime = 0;

        private const int ONE_SHAKE_FRAME_AMOUNT = 2; //Stun時の振動をどれだけ細かくするか
        private const float SHAKING_SPEED = 5; //Stun時の振動の速さ
        private const float STUN_DURATION = 2; //Stunの長さ
        private const float SPEED = 3; //移動速度

        public bool isStunning = false;

        public PlayerPositionController(PauseController pauseController) : base(pauseController) { }

        public void ChangeVelocity()
        {
            if (!pauseController.isGameGoing)
            {
                velocity = Vector3.zero;
                return;
            }

            if (isStunning)
            {
                Stun();
                return;
            }

            //速度の設定
            velocity = new Vector3(
                InputController.horizontalKey * SPEED,
                InputController.verticalKey * SPEED,
                0);
        }

        private void Stun()
        {
            //微小振動させる
            if (_stunFrameCount % ONE_SHAKE_FRAME_AMOUNT * 2 == 0)
            {
                _shakingVector = new Vector3(Random.value * SHAKING_SPEED, Random.value * SHAKING_SPEED, 0);
                velocity = -_shakingVector;
                _stunFrameCount = 0;
            }
            else if (_stunFrameCount % ONE_SHAKE_FRAME_AMOUNT * 2 == ONE_SHAKE_FRAME_AMOUNT)
            {

                velocity = _shakingVector;
            }

            _stunFrameCount++;
            _stunTime += Time.deltaTime;

            //一定時間振動したら元に戻る
            if (_stunTime > STUN_DURATION)
            {
                isStunning = false;
                _stunTime = 0;
            }
        }
    }
}
