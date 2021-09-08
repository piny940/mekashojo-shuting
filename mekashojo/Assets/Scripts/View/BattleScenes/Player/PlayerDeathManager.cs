using UnityEngine;

namespace View
{
    public class PlayerDeathManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Controller.BattleScenesController.playerStatusManager.OnIsDyingChanged.AddListener((bool isDying) =>
            {
                if (isDying)
                {
                    // ここは後々死んだ時の演出を追加していくと思われる
                    SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageFailedScene);
                }
            });
        }
    }
}
