using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonForBattleScenes_scr : MonoBehaviour
{
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;

    /// <summary>
    /// ポーズする(アニメーションなし)<br></br>
    /// Updateに入れる
    /// </summary>
    /// <param name="isPausing"></param>
    /// <param name="rigidbody2D"></param>
    /// <param name="savedVelocity"></param>
    public void Pause(Rigidbody2D rigidbody2D, ref bool isPausing, ref Vector3 savedVelocity)
    {
        //ポーズし始めた時
        if (!_startCount.hasStarted && !isPausing)
        {
            //速度の保存
            savedVelocity = rigidbody2D.velocity;

            //停止
            rigidbody2D.velocity = new Vector3(0, 0, 0);

            isPausing = true;

            return;
        }

        //ポーズし終わった時
        if (_startCount.hasStarted && isPausing)
        {
            rigidbody2D.velocity = savedVelocity;
            isPausing = false;
        }


    }


    /// <summary>
    /// ポーズする(アニメーションあり)<br></br>
    /// Updateに入れる
    /// </summary>
    /// <param name="rigidbody2D"></param>
    /// <param name="isPausing"></param>
    /// <param name="savedVelocity"></param>
    /// <param name="animator"></param>
    public void Pause(Rigidbody2D rigidbody2D, ref bool isPausing, ref Vector3 savedVelocity, Animator animator)
    {
        //ポーズし始めた時
        if (!_startCount.hasStarted && !isPausing)
        {
            //速度の保存
            savedVelocity = rigidbody2D.velocity;

            //停止
            rigidbody2D.velocity = new Vector3(0, 0, 0);
            animator.SetBool("hasStarted", false);

            isPausing = true;

            return;
        }

        //ポーズし終わった時
        if (_startCount.hasStarted && isPausing)
        {
            rigidbody2D.velocity = savedVelocity;
            isPausing = false;
            animator.SetBool("hasStarted", true);
        }


    }
}
