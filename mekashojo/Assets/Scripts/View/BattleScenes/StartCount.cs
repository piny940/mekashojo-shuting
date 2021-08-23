using UnityEngine;

namespace View
{
    public class StartCount : MonoBehaviour
    {
        [SerializeField, Header("StartCountNumber__1を入れる")] private GameObject _number1;
        [SerializeField, Header("StartCountNumber__2を入れる")] private GameObject _number2;
        [SerializeField, Header("StartCountNumber__3を入れる")] private GameObject _number3;

        private int __displayingNumber = 0;
        private int _displayingNumber
        {
            get { return __displayingNumber; }
            set
            {
                __displayingNumber = value;
                _number1.SetActive(value == 1);
                _number2.SetActive(value == 2);
                _number3.SetActive(value == 3);
            }
        }

        void Start()
        {
            _displayingNumber = 0;

            Controller.BattleScenesClassController.pauseController.OnStartTimeCounterChanged.AddListener((float startTimeCounter) =>
            {
                //カウントダウンを表示する
                if (startTimeCounter > 0 && startTimeCounter <= 1 && _displayingNumber != 3)
                {
                    _displayingNumber = 3;
                    return;
                }
                else if (startTimeCounter > 1 && startTimeCounter <= 2 && _displayingNumber != 2)
                {
                    _displayingNumber = 2;
                    return;
                }
                else if (startTimeCounter > 2 && startTimeCounter <= 3 && _displayingNumber != 1)
                {
                    _displayingNumber = 1;
                    return;
                }
                else if (startTimeCounter == 0)
                {
                    _displayingNumber = 0;
                }
            });
        }
    }
}
