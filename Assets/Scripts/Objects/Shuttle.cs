using UnityEngine;

public class Shuttle : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mark;
    [SerializeField] private float speed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] public float maxHeight;
    [SerializeField] private AudioSource _ASHit;
    [SerializeField] private AudioClip _ACHit;
    [SerializeField] private AudioSource _ASServe;
    [SerializeField] private AudioClip _ACServe;
    [SerializeField] private AudioSource _ASSmash;
    [SerializeField] private AudioClip _ACSmash;
    [SerializeField] private ParticleSystem _smashEffect;
    [SerializeField] private TrailRenderer _trailRenderer;
    private float normalheight;
    private bool _setheight = false;
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
    GameManager _gameManager;
    Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        builder = FindObjectOfType<CourtBuilder>();
        _player = FindObjectOfType<Player>();
        _marker = FindObjectOfType<Marker>();
        _enemy = FindObjectOfType<Enemy>();
        _gameManager = FindObjectOfType<GameManager>();
        _tr = transform;
        first = true;
        normalheight = maxHeight;
        _trailRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Racket") && !first && !drop)
        {
            _ASHit.PlayOneShot(_ACHit);
            hit = true;
            receive = false;
            _enemy.ProbabilityCalculation();
            _marker.MarkerSetting = false;
            Debug.Log("ヒット！");
            _trailRenderer.enabled = true;
        }
        if (other.CompareTag("Net"))
        {
            velocity = Vector3.zero;
            hit = false;
            receive = false;
            _trailRenderer.enabled = false;
        }
        if ((other.CompareTag("Out") || other.CompareTag("Floor")) && !restart)
        {
            _trailRenderer.enabled = false;
            if (other.gameObject.layer == 7)//OUT
            {
                Collider targetCol = builder.floor.gameObject.GetComponent<Collider>();
                Bounds b = targetCol.bounds;
                bool IN =(_tr.position.x >= b.min.x && _tr.position.x <= b.max.x)&&
                    ( _tr.position.z >= b.min.z && _tr.position.z <= b.max.z);
                if (IN)
                {
                    HitLayer();
                }
                else
                {
                    OutLayer();
                }
            }
            else if(other.gameObject.layer == 8)//IN
            {
                HitLayer();
            }
        }
        if (other.CompareTag("Enemy"))
        {
            _ASHit.PlayOneShot(_ACHit);
            hit = false;
            drop = false;
            receive = true;
            _enemy.ChaseMode = false;
            _marker.MarkerSetting = true;
            Debug.Log("反撃");
            _trailRenderer.enabled = true;
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
                if (_player.jumping && !_setheight)
                {
                    HeightReset(1.5f);//スマッシュ
                    _smashEffect.transform.rotation =Quaternion.LookRotation(-direction);
                    _smashEffect.Play();
                    _ASSmash.PlayOneShot(_ACSmash);
                }
                velocity.y = Mathf.Sqrt(2f * gravity * maxHeight);
            }
            velocity.y -= gravity * Time.deltaTime;
            _tr.position += new Vector3(0f, velocity.y * Time.deltaTime, 0f);
            if (_tr.position.y <= 0.010f)//シャトルの接地判定
            {
                _tr.position = new Vector3(_tr.position.x, 0f, _tr.position.z);
                velocity.y = 0f;
                drop = true;
            }
            else
            {
                drop = false;
            }
            if (receive)//相手に打ち返される
            {
                Receive();
            }
            if (!hit && !receive && drop && restart)//リスタート
            {
                _marker.Set();
                restart = false;
                first = true;
            }
            if (drop)
            {
                hit = false;
                receive = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Serve();
                _player.playerMove = true;
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
        maxHeight = normalheight;
        _setheight = false;
        Vector3 direction = (randomPos - _tr.position).normalized;
        _tr.position += direction * speed * Time.deltaTime;
        velocity.y = Mathf.Sqrt(2f * gravity * maxHeight);
    }
    /// <summary>
    /// サーブ
    /// </summary>
    private void Serve()
    {
        _ASServe.PlayOneShot(_ACServe);
        _player.Setup();
        _marker.Set();
        _tr.position = new Vector3(0f, 2f, -4.8f);
        first = false;
    }
    /// <summary>
    /// boolの初期化＆動き止める
    /// </summary>
    private void ResetState()
    {
        hit = false;
        receive = false;
        restart = true;
        velocity = Vector3.zero;
    }
    /// <summary>
    /// INの処理
    /// </summary>
    private void HitLayer()
    {
        if (_tr.position.z > 0)
        {
            _gameManager.GetPoint();
        }
        else
        {
            _gameManager.LostPoint();
        }
        Debug.Log("IN");
        ResetState();
    }
    /// <summary>
    /// OUTの処理
    /// </summary>
    private void OutLayer()
    {
        if (_tr.position.z > 0)
        {
            _gameManager.LostPoint();
        }
        else
        {
            _gameManager.GetPoint();
        }
        Debug.Log("OUT");
        ResetState();
    }
    /// <summary>
    /// シャトルの高さ調整
    /// </summary>
    /// <param name="Height"></param>
    private void HeightReset(float Height)
    {
        maxHeight = Height;
        _setheight = true;
    }
}
