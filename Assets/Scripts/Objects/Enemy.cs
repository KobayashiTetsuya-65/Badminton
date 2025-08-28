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

        //接地判定
        if (_CC.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y -= _gravity * Time.deltaTime;
        if (ChaseMode)
        {   
            _velocity.x = (_Pos.x - _tr.position.x) * _speed;
            _velocity.z = (_Pos.z - _tr.position.z) * _speed;
        }
        else
        {
            _velocity.x = (0f - _tr.position.x) * _speed /2f;
            _velocity.z = (5f - _tr.position.z) * _speed /2f;
        }
        Vector3 finalMove = _velocity;
        _CC.Move(finalMove * Time.deltaTime);
        
    }
    /// <summary>
    /// エネミーの返球率の確率演算
    /// </summary>
    public void ProbabilityCalculation()
    {
        _maxRate = 100;
        switch (_Lv)
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
        switch(Mathf.Sqrt((Math.Abs(_Pos.x - _tr.position.x)* Math.Abs(_Pos.x - _tr.position.x))+
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
