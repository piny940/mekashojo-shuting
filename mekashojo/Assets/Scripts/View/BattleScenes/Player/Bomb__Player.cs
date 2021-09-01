using UnityEngine;

namespace View
{
    public class Bomb__Player : MonoBehaviour
    {
        [SerializeField, Header("BombFire__Playerを入れる")] private GameObject _bombFire;

        void Start()
        {
            _bombFire.SetActive(Controller.BattleScenesController.bomb__Player.isUsingBomb);
            _bombFire.transform.localScale = new Vector3(0, 0, 1);

            Controller.BattleScenesController.bomb__Player.OnIsUsingBombChanged.AddListener((bool isBombActive) =>
            {
                _bombFire.SetActive(isBombActive);
            });

            Controller.BattleScenesController.bomb__Player.OnBombSizeChanged.AddListener((float bombSize) =>
            {
                _bombFire.transform.localScale = new Vector3(bombSize, bombSize, 1);
            });
        }
    }
}
