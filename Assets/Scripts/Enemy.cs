using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float speed = 3f;
    public bool ChaseMode = true;
    private Vector3 Pos;
    private Vector3 velocity;
    private CharacterController _CC;
    private Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        _CC = GetComponent<CharacterController>();
        _tr = GetComponent<Transform>();
        _tr.position = new Vector3(0f, 1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        //ê⁄ínîªíË
        if (_CC.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.deltaTime;
        if (ChaseMode)
        {
            Pos.x = marker.transform.position.x;
            Pos.z = marker.transform.position.z;
            velocity.x = (Pos.x - _tr.position.x) * speed;
            velocity.z = (Pos.z - _tr.position.z) * speed;
        }
        else
        {
            velocity.x = (0f - _tr.position.x) * speed/2f;
            velocity.z = (5f - _tr.position.z) * speed/2f;
        }
        Vector3 finalMove = velocity;
        _CC.Move(finalMove * Time.deltaTime);
        
    }
}
