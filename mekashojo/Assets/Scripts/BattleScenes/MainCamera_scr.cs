using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_scr : MonoBehaviour
{
    [SerializeField, Header("Playerを入れる")] GameObject _player;
    [SerializeField, Header("定位置との距離に応じた、定位置に近づく速さ")] AnimationCurve _approachSpeed;
    Vector3 _fromPlayerToCamera;

    private void Start()
    {
        _fromPlayerToCamera = transform.position - _player.transform.position;
    }
    void Update()
    {
        float distance = Vector3.Magnitude(transform.position - (_player.transform.position + _fromPlayerToCamera));//定位置との距離
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position + _fromPlayerToCamera, _approachSpeed.Evaluate(distance) * Time.deltaTime);

    }
}
