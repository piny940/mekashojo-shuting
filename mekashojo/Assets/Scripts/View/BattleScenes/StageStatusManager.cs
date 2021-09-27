using System.Collections.Generic;
using System.Linq;
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
        private const float BOSS_DIE_EXPLOTION_SPEED = 20; // 大きくなっていく速さ
        private const float BOSS_DIE_EXPLOTION_TIME = 1; // 薄くなり始めるまでの時間
        private const float BOSS_DIE_EXPLOTION_FADE_TIME = 1.2f; // 薄くなっていく時間
        private const float BOSS_DIE_EXPLOTION_FADE_SPEED = 0.5f; // 薄くなっていく速さ
        private const float BOSS_DIE_BGM_VOLUME_RATE = 0.5f;


        // KeepOutLineの符号付きの速さ
        private readonly Dictionary<keepOutLineType, float> _lineSpeed
            = new Dictionary<keepOutLineType, float>()
            {
                {keepOutLineType.Top, -5 },
                {keepOutLineType.MiddleTop, 10 },
                {keepOutLineType.MiddleBottom, 10 },
                {keepOutLineType.Bottom, -5 },
            };

        // プログラム上、1番と2番のラインの長さは同じにしないといけない
        [SerializeField, Header("Bossを入れる")] private GameObject _boss;
        [SerializeField, Header("BossDieDirectionを入れる")] private GameObject _bossDieDirection;
        [SerializeField, Header("WhiteFlashを入れる")] private GameObject _whiteFlash;
        [SerializeField, Header("KeepOutLine__Top1を入れる")] private GameObject _keepOutLine__Top1;
        [SerializeField, Header("KeepOutLine__Top2を入れる")] private GameObject _keepOutLine__Top2;
        [SerializeField, Header("KeepOutLine__MiddleTop1を入れる")] private GameObject _keepOutLine__MiddleTop1;
        [SerializeField, Header("KeepOutLine__MiddleTop2を入れる")] private GameObject _keepOutLine__MiddleTop2;
        [SerializeField, Header("KeepOutLine__MiddleBottom1を入れる")] private GameObject _keepOutLine__MiddleBottom1;
        [SerializeField, Header("KeepOutLine__MiddleBottom2を入れる")] private GameObject _keepOutLine__MiddleBottom2;
        [SerializeField, Header("KeepOutLine__Bottom1を入れる")] private GameObject _keepOutLine__Bottom1;
        [SerializeField, Header("KeepOutLine__Bottom2を入れる")] private GameObject _keepOutLine__Bottom2;
        [SerializeField, Header("BossDieBeam1を入れる")] private GameObject _bossDieBeam1;
        [SerializeField, Header("BossDieBeam2を入れる")] private GameObject _bossDieBeam2;
        [SerializeField, Header("BossDieBeam3を入れる")] private GameObject _bossDieBeam3;
        [SerializeField, Header("BossDieBeam4を入れる")] private GameObject _bossDieBeam4;
        [SerializeField, Header("BossDieBeam5を入れる")] private GameObject _bossDieBeam5;
        [SerializeField, Header("BossDieExplotionを入れる")] private GameObject _bossDieExplotion;
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
        private SpriteRenderer _bossDieExplotionRenderer;
        private Image _whiteFlashImage;
        private Dictionary<keepOutLineType, List<GameObject>> _keepOutLines;
        private Dictionary<keepOutLineType, List<RectTransform>> _lineRectTransforms
            = new Dictionary<keepOutLineType, List<RectTransform>>();

        private Dictionary<keepOutLineType, float> _lineLengths
            = new Dictionary<keepOutLineType, float>();

        private Dictionary<keepOutLineType, float> _defaultPositions__x
            = new Dictionary<keepOutLineType, float>();

        private List<GameObject> _bossDieBeams;

        // ボスが死ぬ時、ビームが1つ出てから次のビームが出るまでの時間
        private readonly List<float> _bossDieBeamTimes
            = new List<float>() { 1, 1, 1, 1, 1 };

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

            _bossDieBeams = new List<GameObject>()
            {
                _bossDieBeam1,
                _bossDieBeam2,
                _bossDieBeam3,
                _bossDieBeam4,
                _bossDieBeam5,
            };

            _whiteFlash.SetActive(false);

            for (int i = 0; i < _bossDieBeams.Count; i++)
            {
                _bossDieBeams[i].SetActive(false);
            }

            _bossDieExplotion.SetActive(false);

            _bossDieExplotionRenderer = _bossDieExplotion.GetComponent<SpriteRenderer>();
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
                        BGMPlayer.bgmPlayer.ChangeBGMVolume(BOSS_DIE_BGM_VOLUME_RATE);
                        _isBossDying = true;
                        Vector3 v = _boss.transform.position;
                        v.z = _bossDieDirection.transform.position.z;
                        _bossDieDirection.transform.position = v;
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

            // ボスにある程度近づいたらボスの出現演出に移る
            if (_boss.transform.position.x - _player.transform.position.x < BOSS_APPEAR_DISTANCE
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
                BGMPlayer.bgmPlayer.ChangeBGM(BGMPlayer.bgmNames.BossBattle);
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

            SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageFailedScene);
            BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.StageFailedScene);
        }

        // ボスの死亡モーション
        private void DirectBossDying()
        {
            if (!_isBossDying) return;

            _bossDieTimer += Time.deltaTime;

            // 放射ビームの処理
            if (_bossDieTimer < _bossDieBeamTimes.Sum())
            {
                for (int i = 0; i < _bossDieBeams.Count; i++)
                {
                    if (_bossDieTimer > _bossDieBeamTimes.GetRange(0, i + 1).Sum()
                        && !_bossDieBeams[i].activeSelf)
                    {
                        _bossDieBeams[i].SetActive(true);
                        SEPlayer.sePlayer.PlayOneShot(_bossDieExplotionSound__Small);
                    }
                }
            }

            // 爆発エフェクトの処理
            if (_bossDieTimer > _bossDieBeamTimes.Sum()
                && _bossDieTimer < _bossDieBeamTimes.Sum() + BOSS_DIE_EXPLOTION_TIME)
            {
                if (!_bossDieExplotion.activeSelf)
                {
                    _bossDieExplotion.SetActive(true);
                    _bossDieExplotion.transform.localScale = new Vector3(0, 0, 1);
                    SEPlayer.sePlayer.PlayOneShot(_bossDieExplotionSound__Large);
                }

                // 爆発エフェクトを少しずつ大きくしていく
                Vector3 v = _bossDieExplotion.transform.localScale;
                v += BOSS_DIE_EXPLOTION_SPEED * Time.deltaTime * new Vector3(1, 1, 0);
                _bossDieExplotion.transform.localScale = v;
            }

            // 爆発エフェクトが薄くなっていく処理
            if (_bossDieTimer > _bossDieBeamTimes.Sum() + BOSS_DIE_EXPLOTION_TIME)
            {
                if (!_hasBossDied)
                {
                    Controller.BattleScenesController.stageStatusManager.ChangeStatus(Model.StageStatusManager.stageStatus.BossDead);
                    for (int i = 0; i < _bossDieBeams.Count; i++)
                    {
                        _bossDieBeams[i].SetActive(false);
                    }
                }

                Color color = _bossDieExplotionRenderer.color;
                color.a -= BOSS_DIE_EXPLOTION_FADE_SPEED * Time.deltaTime;
                _bossDieExplotionRenderer.color = color;
            }

            // ボス死亡演出の終了
            if (_bossDieTimer > _bossDieBeamTimes.Sum() + BOSS_DIE_EXPLOTION_TIME + BOSS_DIE_EXPLOTION_FADE_TIME)
            {
                // TODO:BGMの大きさの変更の仕方は、設定画面ができてからまた調整する
                BGMPlayer.bgmPlayer.ChangeBGMVolume(1 / BOSS_DIE_BGM_VOLUME_RATE);
                BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.StageClearScene);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageClearScene);
            }
        }
    }
}
