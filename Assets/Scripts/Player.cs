using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float gravity = 9.8f;
    private CharacterController _CC;
    private Transform _tr;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        _CC = GetComponent<CharacterController>();
        _tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, 0, v);
        if (direction.magnitude > 1f)
        {
            direction = direction.normalized;
        }

        //ê⁄ínîªíË
        if (_CC.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump")&& _CC.isGrounded)
        {

            velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
        }
        velocity.y -= gravity * Time.deltaTime;
        Vector3 finalMove = (direction * speed) + velocity;
        _CC.Move(finalMove * Time.deltaTime);
    }
}
