using UnityEngine;

namespace View
{
    // プレイヤーの弾が敵に当たると、プレイヤーの弾はまず敵のIDを取得し、そのIDを通して
    // 敵のModelクラスのインスタンスへとアクセスする。
    // このクラスで、プレイヤーの弾がIDを取得できるようIDを保持しておく
    public class EnemyIDContainer : MonoBehaviour
    {
        public int id { get; set; }
    }
}
