using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        _CC = GetComponent<CharacterController>();
        _tr = transform;
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
            if (direction.magnitude > 1f)
            {
                direction = direction.normalized;
            }

            //接地判定
            if (_CC.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && _CC.isGrounded)
            {
                velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
            velocity.y -= gravity * Time.deltaTime;
            Vector3 finalMove = (direction * speed) + velocity;
            _CC.Move(finalMove * Time.deltaTime);

            Vector3 pos = _tr.position;
            pos.x = Mathf.Clamp(pos.x, -4f, 4f);
            pos.z = Mathf.Clamp(pos.z, -8f, 8f);
            _tr.position = pos;
        }
        
    }
    public void Setup()
    {
        _CC.enabled = false;
        _tr.position = new Vector3(0f, 0.5f, -5f);
        velocity = Vector3.zero;
        _CC.enabled = true;
        Debug.Log("リスタート");
    }
}
