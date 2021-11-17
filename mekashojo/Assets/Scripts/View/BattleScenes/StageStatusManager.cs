using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StageStatusManager : MonoBehaviour
    {
        // プレイヤーがボスにどれくらい近づいたらボスの出現演出を始めるか
        private const float BOSS_APPEAR_DISTANCE = 20;

        // ボス出現演出の長さ
        private const float BOSS_APPEAR_TIME = 3;

        // ボス死亡時の爆発エフェクト用の定数
        private const float BOSS_DIE_EXPLOTION_TIME = 5;
        private const float BOSS_DIE_EXPLOTION_FADE_TIME = 2;
        private const int EXPLOTION_AMOUNT = 4;

        // KeepOutLineの符号付きの速さ
        private readonly Dictionary<keepOutLineType, float> _lineSpeed
            = new Dictionary<keepOutLineType, float>()
            {
                {keepOutLineType.Top, -5 },
                {keepOutLineType.MiddleTop, 10 },
                {keepOutLineType.MiddleBottom, 10 },
                {keepOutLineType.Bottom, -5 },
            };

        [SerializeField, Header("ステージ名")] private Model.ProgressData.stageName _stageName;
        // プログラム上、1番と2番のラインの長さは同じにしないといけない
        [SerializeField, Header("Bossを入れる")] private GameObject _boss;
        [SerializeField, Header("WhiteFlashを入れる")] private GameObject _whiteFlash;
        [SerializeField, Header("KeepOutLine__Top1を入れる")] private GameObject _keepOutLine__Top1;
        [SerializeField, Header("KeepOutLine__Top2を入れる")] private GameObject _keepOutLine__Top2;
        [SerializeField, Header("KeepOutLine__MiddleTop1を入れる")] private GameObject _keepOutLine__MiddleTop1;
        [SerializeField, Header("KeepOutLine__MiddleTop2を入れる")] private GameObject _keepOutLine__MiddleTop2;
        [SerializeField, Header("KeepOutLine__MiddleBottom1を入れる")] private GameObject _keepOutLine__MiddleBottom1;
        [SerializeField, Header("KeepOutLine__MiddleBottom2を入れる")] private GameObject _keepOutLine__MiddleBottom2;
        [SerializeField, Header("KeepOutLine__Bottom1を入れる")] private GameObject _keepOutLine__Bottom1;
        [SerializeField, Header("KeepOutLine__Bottom2を入れる")] private GameObject _keepOutLine__Bottom2;
        [SerializeField, Header("BossDieExplotionを入れる")] private GameObject _bossDieExplotion;
        [SerializeField, Header("ボスが死んだ後に落ちるひらひらを入れる")] private GameObject _bossDieShower;
        [SerializeField, Header("ボスの小さい爆発音を入れる")] private AudioClip _bossDieExplotionSound__Small;
        [SerializeField, Header("ボスの大きい爆発音を入れる")] private AudioClip _bossDieExplotionSound__Large;

        private GameObject _player;
        private bool _isBossAppearing = false;
        private bool _isPlayerDying = false;
        private bool _isBossDying = false;
        private bool _hasBossAppeared = false;
        private float _bossAppearTimer = 0;
        private float _bossDieTimer = 0;
        private bool _hasBossDied = false;
        private bool _hasExploded = false;
        private int _explotionNumber = 1;
        private Image _whiteFlashImage;
        private Dictionary<keepOutLineType, List<GameObject>> _keepOutLines;
        private Dictionary<keepOutLineType, List<RectTransform>> _lineRectTransforms
            = new Dictionary<keepOutLineType, List<RectTransform>>();

        private Dictionary<keepOutLineType, float> _lineLengths
            = new Dictionary<keepOutLineType, float>();

        private Dictionary<keepOutLineType, float> _defaultPositions__x
            = new Dictionary<keepOutLineType, float>();

        private enum keepOutLineType
        {
            Top,
            MiddleTop,
            MiddleBottom,
            Bottom,
        }

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag(TagManager.TagNames.BattleScenes__Player.ToString());
            _whiteFlashImage = _whiteFlash.GetComponent<Image>();

            SetLineDictionary();

            _whiteFlash.SetActive(false);
            _bossDieExplotion.SetActive(false);
            _bossDieShower.SetActive(false);
        }

        void Start()
        {
            SetLineConfig();

            Controller.BattleScenesController.stageStatusManager.OnCurrentStageStatusChanged.AddListener(status =>
            {
                // 一旦全てfalseにする
                _isBossAppearing = false;
                _isBossDying = false;
                _isPlayerDying = false;

                switch (status)
                {
                    case Model.StageStatusManager.stageStatus.BossAppearing:
                        _isBossAppearing = true;
                        BGMPlayer.bgmPlayer.ChangeBGM(BGMPlayer.bgmNames.BossAppearing);
                        break;

                    case Model.StageStatusManager.stageStatus.PlayerDying:
                        _isPlayerDying = true;
                        break;

                    case Model.StageStatusManager.stageStatus.BossDying:
                        _isBossDying = true;
                        Vector3 v = _boss.transform.position;
                        break;

                    case Model.StageStatusManager.stageStatus.BossDead:
                        _isBossDying = true;
                        break;
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            CheckPlayerPosition();

            DirectBossAppearing();

            DirectPlayerDying();

            DirectBossDying();
        }

        private void CheckPlayerPosition()
        {
            if (!Controller.BattleScenesController.stageStatusManager.isGameGoing)
                return;

            bool isLastStage = _stageName == Model.ProgressData.stageName.LastStage;

            // LastStageの場合はボスの出現演出を行わない
            if (isLastStage && !_hasBossAppeared)
            {
                Controller.BattleScenesController.stageStatusManager.ChangeStatus(Model.StageStatusManager.stageStatus.BossBattle);
                _hasBossAppeared = true;
                return;
            }

            // ボスにある程度近づいたらボスの出現演出に移る
            if ((_boss.transform.position.x - _player.transform.position.x < BOSS_APPEAR_DISTANCE
                    || isLastStage)
                && !_hasBossAppeared)
            {
                Controller.BattleScenesController.stageStatusManager.ChangeStatus(Model.StageStatusManager.stageStatus.BossAppearing);
                _hasBossAppeared = true;
            }
        }

        private void DirectBossAppearing()
        {
            if (!Controller.BattleScenesController.stageStatusManager.isGameGoing
                || !_isBossAppearing)
                return;

            if (!_whiteFlash.activeSelf)
            {
                _whiteFlash.SetActive(true);

                foreach (keepOutLineType type in System.Enum.GetValues(typeof(keepOutLineType)))
                {
                    for (int i = 0; i < _keepOutLines[type].Count; i++)
                    {
                        _keepOutLines[type][i].SetActive(true);
                    }

                    // 前側にある方のラインのx座標を定位置として保存
                    bool isGoingRight = _lineSpeed[type] > 0;

                    float x;
                    if (isGoingRight)
                    {
                        x = Mathf.Min(
                            _keepOutLines[type][0].transform.position.x,
                            _keepOutLines[type][1].transform.position.x
                            );
                    }
                    else
                    {
                        x = Mathf.Max(
                            _keepOutLines[type][0].transform.position.x,
                            _keepOutLines[type][1].transform.position.x
                            );
                    }

                    _defaultPositions__x.Add(type, x);
                }
            }

            _bossAppearTimer += Time.deltaTime;

            // KeepOutLineの処理
            foreach (keepOutLineType type in System.Enum.GetValues(typeof(keepOutLineType)))
            {
                // 一定量移動させる
                for (int i = 0; i < _keepOutLines[type].Count; i++)
                {
                    Vector3 v = _keepOutLines[type][i].transform.position;
                    v.x += _lineSpeed[type] * Time.deltaTime;
                    _keepOutLines[type][i].transform.position = v;
                }

                // 進行方向にある方のKeepOutLineのIndexを確認する
                bool isLine0Right
                    = _keepOutLines[type][0].transform.position.x
                        > _keepOutLines[type][1].transform.position.x;
                bool isGoingRight = _lineSpeed[type] > 0; // 右に向かって進んでいるか
                int frontLineIndex = isLine0Right ^ !isGoingRight ? 0 : 1;

                // KeepOutLine一つ分だけ移動したら元の位置に戻る
                // 左に向かって進んでいる場合は、KeepOutLineのx座標が初めの位置よりも
                // 左にライン２本分進んだら処理する
                // 右に向かって進んでいる場合は、右にライン２本分進んだら処理する
                if ((_keepOutLines[type][frontLineIndex].transform.position.x
                        > _defaultPositions__x[type] + _lineLengths[type] * 2
                        && isGoingRight)
                    || (_keepOutLines[type][frontLineIndex].transform.position.x
                        < _defaultPositions__x[type] - _lineLengths[type] * 2
                        && !isGoingRight))
                {
                    Vector3 v = _keepOutLines[type][frontLineIndex].transform.position;
                    v.x = _defaultPositions__x[type];
                    _keepOutLines[type][frontLineIndex].transform.position = v;
                }
            }

            // Flashの処理
            _whiteFlashImage.color = new Color(1, 1, 1, 1 - _bossAppearTimer % 1);

            // ボス演出終了時の処理
            if (_bossAppearTimer > BOSS_APPEAR_TIME)
            {
                Controller.BattleScenesController.stageStatusManager.ChangeStatus(Model.StageStatusManager.stageStatus.BossBattle);

                foreach (keepOutLineType type in System.Enum.GetValues(typeof(keepOutLineType)))
                {
                    _keepOutLines[type][0].SetActive(false);
                    _keepOutLines[type][1].SetActive(false);
                }

                _whiteFlash.SetActive(false);

                // BGMを変える
                if (_stageName == Model.ProgressData.stageName.LastStage)
                {
                    BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.LastStage);
                }
                else
                {
                    BGMPlayer.bgmPlayer.ChangeBGM(BGMPlayer.bgmNames.BossBattle);
                }
            }
        }

        private void SetLineDictionary()
        {
            // KeepOutLineのGameObjectは辞書として持っておく
            _keepOutLines = new Dictionary<keepOutLineType, List<GameObject>>()
            {
                {
                    keepOutLineType.Top,
                    new List<GameObject>(){ _keepOutLine__Top1, _keepOutLine__Top2}
                },
                {
                    keepOutLineType.MiddleTop,
                    new List<GameObject>(){ _keepOutLine__MiddleTop1, _keepOutLine__MiddleTop2}
                },
                {
                    keepOutLineType.MiddleBottom,
                    new List<GameObject>(){ _keepOutLine__MiddleBottom1, _keepOutLine__MiddleBottom2}
                },
                {
                    keepOutLineType.Bottom,
                    new List<GameObject>(){ _keepOutLine__Bottom1, _keepOutLine__Bottom2}
                },
            };
        }

        private void SetLineConfig()
        {
            // KeepOutLineを全てに対する操作
            foreach (keepOutLineType type in System.Enum.GetValues(typeof(keepOutLineType)))
            {
                _lineRectTransforms.Add(type, new List<RectTransform>());

                for (int i = 0; i < _keepOutLines[type].Count; i++)
                {
                    // 全て非アクティブにする
                    _keepOutLines[type][i].SetActive(false);

                    // RectTransformを取得・辞書に格納
                    RectTransform rectTransform
                        = _keepOutLines[type][i].GetComponent<RectTransform>();

                    _lineRectTransforms[type].Add(rectTransform);
                }

                // 2つのKeepOutLineの大きさは等しくないといけないのでサイズのチェックを行う
                if (_lineRectTransforms[type][1].sizeDelta != _lineRectTransforms[type][0].sizeDelta)
                    throw new System.Exception();

                // KeepOutLineの大きさが等しいことが確認できたので、その長さは2つの
                // KeepOutLineのx座標の差に等しい
                float gap = _keepOutLines[type][0].transform.position.x - _keepOutLines[type][1].transform.position.x;
                _lineLengths[type] = gap > 0 ? gap : -gap;
            }
        }

        private void DirectPlayerDying()
        {
            if (!_isPlayerDying) return;

            // TODOプレイヤーの死亡モーション
            SEPlayer.sePlayer.Stop();
            SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageFailedScene, true);
        }

        // ボスの死亡モーション
        private void DirectBossDying()
        {
            if (!_isBossDying) return;

            _bossDieTimer += Time.deltaTime;

            // EXPLOTION_AMOUNTの数だけ小さい爆発音を鳴らす(1秒おき)
            if (_bossDieTimer > _explotionNumber && _explotionNumber < EXPLOTION_AMOUNT + 2)
            {
                _explotionNumber++;
                SEPlayer.sePlayer.PlayOneShot(_bossDieExplotionSound__Small);
            }

            // 爆発エフェクトの処理
            if (!_hasExploded)
            {
                _hasExploded = true;
                _bossDieExplotion.SetActive(true);
                _bossDieExplotion.transform.position = _boss.transform.position;
                _bossDieShower.transform.position = _boss.transform.position;
                SEPlayer.sePlayer.Stop();
            }

            if (_bossDieTimer > BOSS_DIE_EXPLOTION_TIME && !_hasBossDied)
            {
                _hasBossDied = true;
                _bossDieShower.SetActive(true);
                SEPlayer.sePlayer.PlayOneShot(_bossDieExplotionSound__Large);
                Controller.BattleScenesController.stageStatusManager.ChangeStatus(Model.StageStatusManager.stageStatus.BossDead);
            }

            // ボス死亡演出の終了
            if (_bossDieTimer > BOSS_DIE_EXPLOTION_TIME + BOSS_DIE_EXPLOTION_FADE_TIME)
            {
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageClearScene, true);
                if ((int)_stageName > (int)Model.ProgressData.progressData.stageClearAchievement)
                {
                    Model.ProgressData.progressData.stageClearAchievement = _stageName;
                }
            }
        }
    }
}
