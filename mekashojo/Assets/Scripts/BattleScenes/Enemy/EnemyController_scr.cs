using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController_scr : MonoBehaviour
{
    [SerializeField, Header("敵の数の上限")] int _maxEnemyAmount;
    [SerializeField, Header("初めにいる敵の数")] int _firstEnemyAmount;
    [SerializeField, Header("敵のオブジェクトの名前")] List<string> _enemyNames;
    [SerializeField, Header("各敵を生成する確率の整数比")] List<int> _produceProbabilityRatios;
    [SerializeField, Header("敵の生成確率曲線")] AnimationCurve _enemyProduceProbabilityCurve;
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    public int EnemyAmount { get; set; }   //今いる敵の数
    int _produceProbabilityRatiosSum;
    const float ENEMY_POSITION_Z = 10;
    const float SCREEN_FRAME_WIDTH = 1;
    Vector3 _cornerPosition__LeftBottom;
    Vector3 _cornerPosition__RightTop;

    

    // Start is called before the first frame update
    void Start()
    {
        //各敵の生成する確率比の合計を求める
        _produceProbabilityRatiosSum = _produceProbabilityRatios.Sum();

        //はじめにいる敵の数
        EnemyAmount = _firstEnemyAmount;
    }

    // Update is called once per frame
    void Update()
    {
        CreateNewEnemy();
    }


    /// <summary>
    /// 敵を生成する
    /// </summary>
    void CreateNewEnemy()
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        //敵を生成するかどうかを確率で決める
        if (Random.value > _enemyProduceProbabilityCurve.Evaluate((float)EnemyAmount / (float)_maxEnemyAmount) * Time.deltaTime)
        {
            return;
        }

        //Listの要素数が正しいか確かめる
        if (_enemyNames.Count != _produceProbabilityRatios.Count)
        {
            throw new System.Exception();
        }

        //画面左下と右上の座標の取得
        _cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //どの敵を生成するかを決める
        float randomValueForEnemyChoosing;
        randomValueForEnemyChoosing = Random.value;

        for(int i = 0; i < _enemyNames.Count; i++)
        {
            if (randomValueForEnemyChoosing <= (float)_produceProbabilityRatios.Take(i + 1).Sum() / (float)_produceProbabilityRatiosSum)
            {
                //敵を生成する
                //敵は画面の右半分に生成する仕様にしている
                //上下左右に「敵が出現しない空間」を設けておかないと敵がスクリーンの端っこに生成されてしまうため、SCREEN_FRAME_WIDTHによってスクリーン端には敵を生成しないようにした
                Instantiate((GameObject)Resources.Load("BattleScenes/" + _enemyNames[i]), new Vector3(Random.Range((_cornerPosition__LeftBottom.x + _cornerPosition__RightTop.x) / 2, _cornerPosition__RightTop.x - SCREEN_FRAME_WIDTH), Random.Range(_cornerPosition__LeftBottom.y + SCREEN_FRAME_WIDTH, _cornerPosition__RightTop.y - SCREEN_FRAME_WIDTH), ENEMY_POSITION_Z), Quaternion.identity);
                EnemyAmount++;
                break;
            }
        }
    }
}
