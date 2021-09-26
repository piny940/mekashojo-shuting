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

        // ボス出現演出中のフレームレート
        // (ボス出現演出中は意図的にフレームレートを下げる)
        private const int FPS_WHILE_BOSS_APPEARING = 10;

        // KeepOutLineの符号付きの速さ
        private readonly Dictionary<keepOutLineType, float> _lineSpeed
            = new Dictionary<keepOutLineType, float>()
            {
                {keepOutLineType.Top, 5 },
                {keepOutLineType.MiddleTop, -10 },
                {keepOutLineType.MiddleBottom, -10 },
                {keepOutLineType.Bottom, 5 },
            };

        // プログラム上、1番と2番のラインの長さは同じにしないといけない
        [SerializeField, Header("WhiteFlashを入れる")] private GameObject _whiteFlash;
        [SerializeField, Header("KeepOutLine__Top1を入れる")] private GameObject _keepOutLine__Top1;
        [SerializeField, Header("KeepOutLine__Top2を入れる")] private GameObject _keepOutLine__Top2;
        [SerializeField, Header("KeepOutLine__MiddleTop1を入れる")] private GameObject _keepOutLine__MiddleTop1;
        [SerializeField, Header("KeepOutLine__MiddleTop2を入れる")] private GameObject _keepOutLine__MiddleTop2;
        [SerializeField, Header("KeepOutLine__MiddleBottom1を入れる")] private GameObject _keepOutLine__MiddleBottom1;
        [SerializeField, Header("KeepOutLine__MiddleBottom2を入れる")] private GameObject _keepOutLine__MiddleBottom2;
        [SerializeField, Header("KeepOutLine__Bottom1を入れる")] private GameObject _keepOutLine__Bottom1;
        [SerializeField, Header("KeepOutLine__Bottom2を入れる")] private GameObject _keepOutLine__Bottom2;

        private GameObject _player;
        private bool _isBossAppearing = false;
        private bool _isPlayerDying = false;
        private bool _isBossDying = false;
        private bool _hasBossAppeared = false;
        private float _bossAppearTimer = 0;
        private Image _whiteFlashImage;
        private Dictionary<keepOutLineType, List<GameObject>> _keepOutLines;
        private Dictionary<keepOutLineType, List<SpriteRenderer>> _lineSpriteRenderers
            = new Dictionary<keepOutLineType, List<SpriteRenderer>>();

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
        }

        void Start()
        {
            _whiteFlash.SetActive(false);

            SetLineDictionary();

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
                        Application.targetFrameRate = FPS_WHILE_BOSS_APPEARING;
                        _isBossAppearing = true;
                        break;

                    case Model.StageStatusManager.stageStatus.PlayerDying:
                        _isPlayerDying = true;
                        break;

                    case Model.StageStatusManager.stageStatus.BossDying:
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
            if (Controller.BattleScenesController.stageSettings.bossPosition.x - _player.transform.position.x < BOSS_APPEAR_DISTANCE
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
                    for (int i = 0; i < _keepOutLines[type].Count; i++)
                        _keepOutLines[type][i].SetActive(true);
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
                bool isGoingLeft = _lineSpeed[type] > 0; // 左に向かって進んでいるか
                int frontLineIndex = isLine0Right ^ isGoingLeft ? 0 : 1;

                // KeepOutLine一つ分だけ移動したら元の位置に戻る
                // 左に向かって進んでいる場合は、KeepOutLineのx座標が初めの位置よりも
                // 左にライン２本分進んだら処理する
                // 右に向かって進んでいる場合は、右にライン２本分進んだら処理する
                if ((_keepOutLines[type][frontLineIndex].transform.position.x
                        > _defaultPositions__x[type] + _lineLengths[type] * 2
                    && isGoingLeft)
                    || (_keepOutLines[type][frontLineIndex].transform.position.x
                        > _defaultPositions__x[type] - _lineLengths[type] * 2
                        && !isGoingLeft))
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

                Application.targetFrameRate = -1;
                _whiteFlash.SetActive(false);
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
                _lineSpriteRenderers.Add(type, new List<SpriteRenderer>());

                for (int i = 0; i < _keepOutLines[type].Count; i++)
                {
                    // 全て非アクティブにする
                    _keepOutLines[type][i].SetActive(false);

                    // SpriteRendererを取得・辞書に格納
                    SpriteRenderer spriteRenderer
                        = _keepOutLines[type][i].GetComponent<SpriteRenderer>();

                    _lineSpriteRenderers[type].Add(spriteRenderer);
                }

                // 左側のラインの初めのx座標
                float x = Mathf.Min(
                        _keepOutLines[type][0].transform.position.x,
                        _keepOutLines[type][1].transform.position.x
                        );
                _defaultPositions__x.Add(type, x);

                // 2つのKeepOutLineの大きさは等しくないといけないのでサイズのチェックを行う
                if (_lineSpriteRenderers[type][1].size != _lineSpriteRenderers[type][0].size)
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

        private void DirectBossDying()
        {
            if (!_isBossDying) return;

            // TODOボスの死亡モーション

            BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.StageClearScene);
            SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageClearScene);
        }
    }
}
