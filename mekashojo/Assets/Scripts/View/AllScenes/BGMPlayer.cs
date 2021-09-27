using UnityEngine;

namespace View
{
    public class BGMPlayer : MonoBehaviour
    {
        public static BGMPlayer bgmPlayer;
        private AudioSource _bgmAudioSource;
        [SerializeField, Header("Title画面のBGMを入れる")] private AudioClip _bgm__TitleScene;
        [SerializeField, Header("Menu画面のBGMを入れる")] private AudioClip _bgm__MenuScene;
        [SerializeField, Header("Stage1画面のBGMを入れる")] private AudioClip _bgm__Stage1;
        [SerializeField, Header("Stage2画面のBGMを入れる")] private AudioClip _bgm__Stage2;
        [SerializeField, Header("Stage3画面のBGMを入れる")] private AudioClip _bgm__Stage3;
        [SerializeField, Header("Stage4画面のBGMを入れる")] private AudioClip _bgm__Stage4;
        [SerializeField, Header("LastStage画面のBGMを入れる")] private AudioClip _bgm__LastStage;
        [SerializeField, Header("StageClear画面のBGMを入れる")] private AudioClip _bgm__StageClearScene;
        [SerializeField, Header("StageFailed画面のBGMを入れる")] private AudioClip _bgm__StageFailed;

        public enum bgmNames
        {
            _none,
            Title,
            Menu,
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            LastStage,
            StageClear,
            StageFailed,
            BossAppearing,
            BossBattle,
        }

        private void Awake()
        {
            if (bgmPlayer == null)
            {
                bgmPlayer = this;
                DontDestroyOnLoad(this.gameObject);
                _bgmAudioSource = GetComponent<AudioSource>();
                _bgmAudioSource.Play();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // BGMを切り替えるか、初めから再生し直す場合によぶ
        // ステージ名から変える場合
        public void ChangeBGM(SceneChangeManager.SceneNames sceneName)
        {
            switch (sceneName)
            {
                case SceneChangeManager.SceneNames.TitleScene:
                    _bgmAudioSource.clip = _bgm__TitleScene;
                    break;

                case SceneChangeManager.SceneNames.MenuScene:
                case SceneChangeManager.SceneNames.EquipmentScene:
                case SceneChangeManager.SceneNames.StageSelectScene:
                case SceneChangeManager.SceneNames.HowToPlayScene:
                case SceneChangeManager.SceneNames.StoryScene:
                    _bgmAudioSource.clip = _bgm__MenuScene;
                    break;

                case SceneChangeManager.SceneNames.Stage1:
                    _bgmAudioSource.clip = _bgm__Stage1;
                    break;

                case SceneChangeManager.SceneNames.Stage2:
                    _bgmAudioSource.clip = _bgm__Stage2;
                    break;

                case SceneChangeManager.SceneNames.Stage3:
                    _bgmAudioSource.clip = _bgm__Stage3;
                    break;

                case SceneChangeManager.SceneNames.Stage4:
                    _bgmAudioSource.clip = _bgm__Stage4;
                    break;

                case SceneChangeManager.SceneNames.LastStage:
                    _bgmAudioSource.clip = _bgm__LastStage;
                    break;

                case SceneChangeManager.SceneNames.StageClearScene:
                    _bgmAudioSource.clip = _bgm__StageClearScene;
                    break;

                case SceneChangeManager.SceneNames.StageFailedScene:
                    _bgmAudioSource.clip = _bgm__StageFailed;
                    break;
            }

            _bgmAudioSource.Play();
        }

        // ファイル名から変える場合
        public void ChangeBGM(bgmNames name)
        {
            _bgmAudioSource.clip = (AudioClip)Resources.Load($"BGM/{name}");
            _bgmAudioSource.Play();
        }

        public void StopBGM()
        {
            _bgmAudioSource.Stop();
        }

        public void ChangeBGMVolume(float rate)
        {
            _bgmAudioSource.volume *= rate;
        }
    }
}
