using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Model
{
    public class RandomChoosing
    {
        /// <summary>
        /// 確率の比とその対象をまとめたDictを入力しその中から一つを決定、対象を返す
        /// </summary>
        public static T ChooseRandomly<T>(Dictionary<T, float> probabilities)
        {
            // 確率比の和
            float totalProbability = probabilities.Values.Sum();

            // 0〜確率比の和の間で乱数を作成
            float randomValue = Random.value * totalProbability;

            // 乱数から各確率を引いていき、0以下になったら終了
            foreach (KeyValuePair<T, float> pair in probabilities)
            {
                randomValue -= pair.Value;
                if (randomValue <= 0)
                {
                    return pair.Key;
                }
            }
            //エラー、ここに来た時はプログラムが間違っている
            throw new System.Exception();
        }
    }
}
