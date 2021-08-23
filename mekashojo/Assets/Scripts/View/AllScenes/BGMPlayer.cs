using UnityEngine;

namespace View
{
    public class BGMPlayer : MonoBehaviour
    {
        public static BGMPlayer bgmPlayer;
        [HideInInspector] public AudioSource bgmAudioSource;

        private void Awake()
        {
            if (bgmPlayer == null)
            {
                bgmPlayer = this;
                DontDestroyOnLoad(this.gameObject);
                bgmAudioSource = GetComponent<AudioSource>();
                bgmAudioSource.Play();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
