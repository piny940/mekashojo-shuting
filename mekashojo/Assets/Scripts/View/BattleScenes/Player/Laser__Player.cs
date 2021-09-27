using UnityEngine;

namespace View
{
    public class Laser__Player : CannonAndLaser__PlayerBase
    {
        [SerializeField, Header("レーザー使用中に鳴らす音声を入れる")] private AudioClip _laserSound;
        private int _soundID;

        void Start()
        {
            fire__Player.SetActive(false);

            Controller.BattleScenesController.laser__Player.OnFiringTargetChanged.AddListener(RotateFire);

            Controller.BattleScenesController.laser__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) =>
            {
                fire__Player.SetActive(isFireVisible);

                if (isFireVisible) _soundID = SEPlayer.sePlayer.Play(_laserSound);
                else SEPlayer.sePlayer.Stop(_soundID);
            });
        }
    }
}
