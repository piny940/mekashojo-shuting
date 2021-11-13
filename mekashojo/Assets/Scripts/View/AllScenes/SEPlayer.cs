using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class SEPlayer : MonoBehaviour
    {
        public static SEPlayer sePlayer;

        private AudioSource audioSource { get; set; }

        private Dictionary<int, AudioSource> _audioSources
            = new Dictionary<int, AudioSource>();

        private int _latestID = 0;

        private void Awake()
        {
            if (sePlayer == null)
            {
                sePlayer = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            audioSource = GetComponent<AudioSource>();
        }

        // 一度だけ鳴らす
        public void PlayOneShot(AudioClip clip)
        {
            if (clip == null) return;
            audioSource.PlayOneShot(clip);
        }

        // 止めるまで鳴らし続ける
        public int Play(AudioClip clip, bool willLoop = true)
        {
            if (clip == null) return -1;

            // IDを取得
            _latestID++;
            int id = _latestID;

            // 音源のコンポーネントを追加してそこからSEを鳴らす
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.loop = willLoop;
            audioSource.Play();

            _audioSources.Add(id, audioSource);
            return id;
        }

        // Playメソッドで鳴らしたSEを止める
        public void Stop(int id = -1)
        {
            // IDを指定しなかったら全てのSEを止める
            if (id == -1)
            {
                for (int i = 0; i <= _latestID; i++)
                {
                    if (_audioSources.ContainsKey(i))
                    {
                        AudioSource audio = _audioSources[i];
                        audio.Stop();
                        _audioSources.Remove(i);
                        Destroy(audio);
                    }
                }

                return;
            }

            // idを指定して止める場合
            if (!_audioSources.ContainsKey(id)) return;

            AudioSource audioSource = _audioSources[id];

            audioSource.Stop();

            _audioSources.Remove(id);

            Destroy(audioSource);
        }
    }
}
