using UnityEngine;

namespace View
{
    public class FiringBulletBase : CollisionBase
    {
        /// <summary>
        /// 弾を指定された速度で発射する
        /// </summary>
        /// <param name="bulletVelocity"></param>
        protected void Fire(Vector3 bulletVelocity, string path)
        {
            //弾の生成
            GameObject bullet = PrefabManager.ProduceObject(path, transform.position);

            //コンポーネントの取得
            Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();

            //null安全性
            if (bulletRigidbody2D == null)
            {
                throw new System.Exception();
            }

            //弾の速度の設定
            bulletRigidbody2D.velocity = bulletVelocity;

            //弾の回転
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(bulletVelocity.x, bulletVelocity.y, 0), new Vector3(0, 0, 1));
            bullet.transform.localEulerAngles = new Vector3(0, 0, theta);
        }
    }
}
