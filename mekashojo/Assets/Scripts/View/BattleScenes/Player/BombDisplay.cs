using UnityEngine;

namespace View
{
    public class BombDisplay : MonoBehaviour
    {
        [SerializeField, Header("HavingBomb1を入れる")] private GameObject _bomb1;
        [SerializeField, Header("HavingBomb2を入れる")] private GameObject _bomb2;
        [SerializeField, Header("HavingBomb3を入れる")] private GameObject _bomb3;

        void Start()
        {
            _bomb1.SetActive(Controller.BattleScenesController.playerStatusManager.bombAmount >= 1);
            _bomb2.SetActive(Controller.BattleScenesController.playerStatusManager.bombAmount >= 2);
            _bomb3.SetActive(Controller.BattleScenesController.playerStatusManager.bombAmount >= 3);

            Controller.BattleScenesController.playerStatusManager.OnBombAmountChanged.AddListener((int bombAmount) =>
            {
                _bomb1.SetActive(bombAmount >= 1);
                _bomb2.SetActive(bombAmount >= 2);
                _bomb3.SetActive(bombAmount >= 3);
            });
        }
    }
}
