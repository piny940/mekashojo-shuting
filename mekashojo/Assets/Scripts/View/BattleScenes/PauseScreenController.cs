using UnityEngine;

namespace View
{
    public class PauseScreenController : MonoBehaviour
    {
        [SerializeField, Header("PauseScreenを入れる")] private GameObject _pauseScreen;

        void Start()
        {
            _pauseScreen.SetActive(false);

            Controller.ModelClassController.pauseController.OnPauseScreenVisibilityChanged.AddListener((bool isPauseScreenActive) =>
            {
                _pauseScreen.SetActive(isPauseScreenActive);
            });
        }
    }
}
