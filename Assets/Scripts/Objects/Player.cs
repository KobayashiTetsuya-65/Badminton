using System.ComponentModel;
using UnityEditor.VersionControl;
using UnityEngine;
public enum CharactorAnimationState
{
    Idle,Walk,Jump,Attack,Damage
}
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public bool playerMove = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float gravity = 9.8f;
    private CharacterController _CC;
    private Transform _tr;
    private Vector3 velocity;
    public bool jumping = false;
    [Header("プレイヤーの設定")]
    [SerializeField] private GameObject _slimeBody;
    [SerializeField] private CharactorAnimationState _currentState;
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _CC = GetComponent<CharacterController>();
        _tr = transform;
        _currentState = CharactorAnimationState.Idle;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(h, 0, v);
            if((h != 0 ||  v != 0) && !jumping)
            {
                _currentState = CharactorAnimationState.Walk;
                _animator.SetFloat("Speed", 1f);
            }
            else
            {
                _currentState = CharactorAnimationState.Idle;
                _animator.SetFloat("Speed", 0f);
            }
            if (direction.magnitude > 1f)
            {
                direction = direction.normalized;
            }
            
            //接地判定
            if (_CC.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                jumping = false;
            }

            if (Input.GetButtonDown("Jump") && !jumping)
            {
                _currentState = CharactorAnimationState.Jump;
                velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
                jumping = true;
            }
            if (jumping)
            {

            }
            velocity.y -= gravity * Time.deltaTime;
            Vector3 finalMove = (direction * speed) + velocity;
            _CC.Move(finalMove * Time.deltaTime);
            //向きの制御
            if (direction.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _tr.rotation = Quaternion.Slerp(_tr.rotation, targetRotation, Time.deltaTime * 10f);
            }

            _animator.SetBool("isJumping", jumping);
            Vector3 pos = _tr.position;
            pos.x = Mathf.Clamp(pos.x, -4f, 4f);
            pos.z = Mathf.Clamp(pos.z, -8f, 8f);
            _tr.position = pos;
        }
        Debug.Log(_currentState);
    }
    public void Setup()
    {
        _CC.enabled = false;
        _tr.position = new Vector3(0f, 0.5f, -5f);
        velocity = Vector3.zero;
        _CC.enabled = true;
        Debug.Log("リスタート");
    }
    private void OnAnimatorMove()
    {

    }
    private void AlertObservers(string message)
    {
        Debug.Log($"AnimationEvent: {message}");
        // 必要ならここで処理を書く
    }
}
