using UnityEngine;

namespace View
{
    public class Cannon__Player : CannonAndLaser__PlayerBase
    {
        [SerializeField, Header("キャノン使用中に鳴らす音声を入れる")] private AudioClip _cannonSound;

        void Start()
        {
            fire__Player.SetActive(false);

            Controller.BattleScenesController.cannon__Player.OnFiringTargetChanged.AddListener(RotateFire);

            Controller.BattleScenesController.cannon__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) =>
            {
                fire__Player.SetActive(isFireVisible);

                if (isFireVisible) SEPlayer.sePlayer.PlayOneShot(_cannonSound);
            });
        }
    }
}
