using UnityEngine;

namespace View
{
    public class Bomb__Player : MonoBehaviour
    {
        [SerializeField, Header("BombFire__Playerを入れる")] private GameObject _bombFire;

        void Start()
        {
            Controller.ModelClassController.bomb__Player.OnIsBombActiveChanged.AddListener((bool isBombActive) =>
            {
                _bombFire.SetActive(isBombActive);
            });

            Controller.ModelClassController.bomb__Player.OnBombSizeChanged.AddListener((float bombSize) =>
            {
                _bombFire.transform.localScale = new Vector3(bombSize, bombSize, 1);
            });
        }
    }
}
