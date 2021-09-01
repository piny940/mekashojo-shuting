using UnityEngine;

namespace View
{
    public class PauseScreenManager : MonoBehaviour
    {
        [SerializeField, Header("PauseScreenを入れる")] private GameObject _pauseScreen;

        void Start()
        {
            _pauseScreen.SetActive(false);

            Controller.BattleScenesController.pauseManager.OnIsPausingChanged.AddListener((bool isPauseScreenActive) =>
            {
                _pauseScreen.SetActive(isPauseScreenActive);
            });
        }
    }
}
