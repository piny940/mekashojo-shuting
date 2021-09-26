using UnityEngine;

namespace View
{
    public class PauseScreenManager : MonoBehaviour
    {
        [SerializeField, Header("PauseScreenを入れる")] private GameObject _pauseScreen;

        void Start()
        {
            _pauseScreen.SetActive(false);

            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                bool isScreenVisible = status == Model.StageStatusManager.stageStatus.IsPausing;
                _pauseScreen.SetActive(isScreenVisible);
            });
        }
    }
}
