using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffectBaseImp : MonoBehaviour
{
    float _time;
    [SerializeField, Header("消滅するまでの時間")] float _disappearTime;

    // Start is called before the first frame update
    protected void Start()
    {
        _time = 0;
    }

    // Update is called once per frame
    protected void Update()
    {
        _time += Time.deltaTime;
        if (_time > _disappearTime)
        {
            Destroy(this.gameObject);
        }
    }
}
