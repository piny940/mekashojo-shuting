using UnityEngine;

namespace View
{
    public class ExplodeEffect : MonoBehaviour
    {
        float _time;
        [SerializeField, Header("消滅するまでの時間")] float _disappearTime;

        protected void Start()
        {
            _time = 0;
        }

        protected void Update()
        {
            if (!Controller.BattleScenesController.pauseManager.isGameGoing) return;

            _time += Time.deltaTime;
            if (_time > _disappearTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
