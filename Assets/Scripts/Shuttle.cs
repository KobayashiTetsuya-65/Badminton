using UnityEngine;

public class Shuttle : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mark;
    [SerializeField] private float speed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] public float maxHeight;
    private bool first = true;
    private bool randomset = false;
    private bool restart = false;
    private bool drop = false;
    public bool hit = false;//自分が跳ね返す時
    public bool receive = false;//敵が跳ね返す時
    private Vector3 velocity;
    public Vector3 randomPos;
    CourtBuilder builder;
    Enemy _enemy;
    Player _player;
    Marker _marker;
    Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        builder = FindObjectOfType<CourtBuilder>();
        _player = FindObjectOfType<Player>();
        _marker = FindObjectOfType<Marker>();
        _enemy = FindObjectOfType<Enemy>();
        _tr = transform;
        first = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Racket") && !first)
        {
            hit = true;
            receive = false;
            _enemy.ChaseMode = true;
            _marker.MarkerSetting = false;
            Debug.Log("ヒット！");
        }
        if (other.CompareTag("Net") || other.CompareTag("Floor"))
        {
            hit = false;
            receive = false;
            restart = true;
            velocity = Vector3.zero;
        }
        if (other.CompareTag("Enemy"))
        {
            hit = false;
            drop = false;
            receive = true;
            _enemy.ChaseMode = false;
            _marker.MarkerSetting = true;
            Debug.Log("反撃");
        }
        if (other.CompareTag("Out"))
        {
            hit = false;
            receive = false;
            restart = true;
            velocity = Vector3.zero;
        }
    }

   // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            if (hit)//打ち返す
            {
                randomset = false;
                Vector3 direction = (mark.transform.position - _tr.position).normalized;
                _tr.position += direction * speed * Time.deltaTime;
                velocity.y = Mathf.Sqrt(2f * gravity * maxHeight);
            }
            velocity.y -= gravity * Time.deltaTime;
            _tr.position += new Vector3(0f, velocity.y * Time.deltaTime, 0f);
            if (_tr.position.y <= 0.03f)//シャトルの接地判定
            {
                _tr.position = new Vector3(_tr.position.x, 0f, _tr.position.z);
                velocity.y = 0f;
                drop = true;
            }
            else
            {
                drop = false;
            }
            if (hit && drop)//敵の所に落ちた時
            {
                hit = false;
                drop = false;
                receive = true;
            }
            if (receive)//相手に打ち返される
            {
                Receive();
            }
            if (!hit && !receive && drop && restart)//リスタート
            {
                
                _marker.Set();
                _player.Setup();
                restart = false;
                first = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Serve();
            }
        }
        
    }
    /// <summary>
    /// 敵からの返球
    /// </summary>
    private void Receive()
    {
        if (!randomset)
        {
            float x = Random.Range(-builder.courtWidth/2f - 0.5f, builder.courtWidth/2f + 0.5f);
            float z = Random.Range(-builder.courtLength/2f -0.5f, 0.5f);
            randomPos = new Vector3(x, 0f, z);
            randomset = true;
        }              
        Vector3 direction = (randomPos - _tr.position).normalized;
        _tr.position += direction * speed * Time.deltaTime;
        velocity.y = Mathf.Sqrt(2f * gravity * maxHeight);
    }
    /// <summary>
    /// サーブ
    /// </summary>
    private void Serve()
    {
        _tr.position = new Vector3(0f, 2f, -4.8f);
        first = false;
    }
}
