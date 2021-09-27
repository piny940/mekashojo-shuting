using UnityEngine;

namespace View
{
    public class PlayerDebuffManager : MonoBehaviour
    {
        [SerializeField, Header("スタン中に鳴らすビリビリ音を入れる")] AudioClip _stunSound;
        private int _stunSoundID;

        // Start is called before the first frame update
        void Start()
        {
            Controller.BattleScenesController.playerDebuffManager.OnIsStunedChanged.AddListener((bool isStuned) =>
            {
                if (isStuned) _stunSoundID = SEPlayer.sePlayer.Play(_stunSound);
                else SEPlayer.sePlayer.Stop(_stunSoundID);
            });
        }
    }
}
