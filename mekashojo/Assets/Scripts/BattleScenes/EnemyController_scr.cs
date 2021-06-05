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
    [SerializeField, Header("MainCameraを入れる")] GameObject _mainCamera;
    int _EnemyAmount;   //今いる敵の数
    int _produceProbabilityRatiosSum;

    // Start is called before the first frame update
    void Start()
    {
        _produceProbabilityRatiosSum = _produceProbabilityRatios.Sum();
        _EnemyAmount = _firstEnemyAmount;
    }

    // Update is called once per frame
    void Update()
    {
        CreateNewEnemy();
    }

    void CreateNewEnemy()
    {
        //敵を生成するかどうかを確率で決める
        if (Random.value > _enemyProduceProbabilityCurve.Evaluate((float)_EnemyAmount / (float)_maxEnemyAmount) * Time.deltaTime)
        {
            return;
        }

        
        if (_enemyNames.Count != _produceProbabilityRatios.Count)
        {
            Debug.Log("_enemyNamesと_enemyProduceProbabilityRatiosの要素数が異なります");
            return;
        }

        
        float _randomValueForEnemyChoosing;
        _randomValueForEnemyChoosing = Random.value;

        for(int i = 0; i < _enemyNames.Count; i++)
        {
            if (_randomValueForEnemyChoosing <= (float)_produceProbabilityRatios[i] / (float)_produceProbabilityRatiosSum)
            {
                Instantiate((GameObject)Resources.Load(_enemyNames[i]), new Vector3(Random.Range(_mainCamera.transform.position.x, _mainCamera.transform.position.x + 8), Random.Range(-3, 4), 0), Quaternion.identity);
                _EnemyAmount++;
                break;
            }
            _randomValueForEnemyChoosing -= (float)_produceProbabilityRatios[i] / (float)_produceProbabilityRatiosSum;
        }
    }
}
