using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0, 2)] public int _Lv;
    [SerializeField] private GameObject _marker;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _speed = 3f;
    [Header("返球率")]
    [Header("Lv.1"), SerializeField] private float _successRate1 = 50f;
    [Header("Lv.2"), SerializeField] private float _successRate2 = 70f;
    [Header("Lv.3"), SerializeField] private float _successRate3 = 90f;
    private float _successRate;
    private float _randomRate;
    private float _maxRate = 100;
    public bool _success;
    public bool ChaseMode = false;
    private Vector3 _Pos;
    private Vector3 _velocity;
    private Vector3 _move;
    private CharacterController _CC;
    private Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        _CC = GetComponent<CharacterController>();
        _tr = GetComponent<Transform>();
        _tr.position = new Vector3(0f, 1f, 5f);
        ChaseMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        _move = Vector3.zero;
        //接地判定
        if (_CC.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y -= _gravity * Time.deltaTime;
        if (ChaseMode)
        {
            Vector3 targetPos = new Vector3(_Pos.x, 0, _Pos.z);
            Vector3 currentPos = new Vector3(_tr.position.x, 0, _tr.position.z);

            float distance = Vector3.Distance(currentPos, targetPos);

            if (distance > 0.03f) // 0.1m以上離れてたら移動
            {
                _move = (targetPos - currentPos).normalized * _speed;
            }
            else
            {
                _move = Vector3.zero; // 到着したら停止
            }
        }
        else
        {
            Vector3 targetPos = new Vector3(0f, 0, 5f); // 初期位置
            Vector3 currentPos = new Vector3(_tr.position.x, 0, _tr.position.z);

            float distance = Vector3.Distance(currentPos, targetPos);

            if (distance > 0.03f) // 近くなったら止める
            {
                _move = (targetPos - currentPos).normalized * _speed / 2f;
            }
            else
            {
                _move = Vector3.zero;
            }
        }
        Vector3 finalMove = _move + new Vector3(0, _velocity.y ,0);
        _CC.Move(finalMove * Time.deltaTime);
        
    }
    /// <summary>
    /// エネミーの返球率の確率演算
    /// </summary>
    public void ProbabilityCalculation()
    {
        _maxRate = 100;
        switch (_Lv)//レベル変更
        {
            case 0:
                _successRate = _successRate1;
                break;
            case 1:
                _successRate = _successRate2;
                break;
            case 2:
                _successRate = _successRate3;
                break;
        }
        _Pos.x = _marker.transform.position.x;
        _Pos.z = _marker.transform.position.z;
        //距離に応じて確率変更
        switch (Mathf.Sqrt((Math.Abs(_Pos.x - _tr.position.x)* Math.Abs(_Pos.x - _tr.position.x))+
            (Math.Abs(_Pos.z - _tr.position.z) * Math.Abs(_Pos.z - _tr.position.z))))
        {
            case < 0.5f:
                _maxRate = 1;
                break;
            case < 2:
                _maxRate = _successRate2;
                break;
            case < 5:
                _maxRate = 100;
                    break;
            case < 100:
                _maxRate = 500;
                break;
        }
        _randomRate = UnityEngine.Random.Range(0, _maxRate);
        if (_randomRate <= _successRate)
        {
            ChaseMode = true;
        }
    }
}
