using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rote = 20f;
    Transform _tr;
    Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _tr = transform;
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            _player.playerMove = false;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            _tr.Translate(moveDirection * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.E))
            {
                _tr.Rotate(Time.deltaTime * rote, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.R))
            {
                _tr.Rotate(Time.deltaTime * -rote, 0f, 0f);
            }
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _player.playerMove = true;
        }

    }
}
