using UnityEngine;

namespace View
{
    public class PlayerDebuffManager : MonoBehaviour
    {
        [SerializeField, Header("スタン中に鳴らすビリビリ音を入れる")] AudioClip _stunSound;
        private int _stunSoundID = -1;

        // Start is called before the first frame update
        void Start()
        {
            Controller.BattleScenesController.playerDebuffManager.OnIsStunedChanged.AddListener((bool isStuned) =>
            {
                if (isStuned)
                {
                    // すでにstunSoundを鳴らしている場合を想定して、一旦音を止めてから
                    // もう一度鳴らす
                    SEPlayer.sePlayer.Stop(_stunSoundID);
                    _stunSoundID = SEPlayer.sePlayer.Play(_stunSound);
                }
                else SEPlayer.sePlayer.Stop(_stunSoundID);
            });
        }
    }
}
