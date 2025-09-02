using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
/// <summary>
/// 敵の弾の落下地点&得点管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject playerobj;
    [SerializeField] private GameObject enemyobj;
    [SerializeField] private TextMeshProUGUI pointTextP;
    [SerializeField] private TextMeshProUGUI pointTextE;
    [SerializeField] private TextMeshProUGUI setpointTextP;
    [SerializeField] private TextMeshProUGUI setpointTextE;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private Image _panelImage;
    [Header("ポイント設定"), SerializeField, Range(1, 21)]
    private int maxPoint;
    [Header("セットカウント設定"), SerializeField, Range(1, 3)]
    private int maxSetPoint;
    [Header("SE")]
    [SerializeField] private AudioSource _ASGetPoint;
    [SerializeField] private AudioClip _ACGetPoint;
    [SerializeField] private AudioSource _ASLostPoint;
    [SerializeField] private AudioClip _ACLostPoint;
    [SerializeField] private AudioSource _ASFinish;
    [SerializeField] private AudioClip _ACFinish;
    private bool reset = false;
    private bool markOnScene = false;
    private int pointP = 0;
    private int pointE = 0;
    private int setPP = 0;
    private int setPE = 0;
    private GameObject currentMaker;
    Shuttle shuttle;
    //Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (shuttle == null)
        {
            shuttle = FindObjectOfType<Shuttle>();
        }
        PointUPdate();
        winText.SetActive(false);
        loseText.SetActive(false);
        _panelImage.DOFade(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shuttle.receive && !markOnScene)
        {
            Vector3 receivemarker = new Vector3(shuttle.randomPos.x, 0f, shuttle.randomPos.z);
            currentMaker = Instantiate(markerPrefab, receivemarker, Quaternion.identity);
            markOnScene = true;
        }
        if (markOnScene)
        {
            if (shuttle.hit)
            {
                Destroy(currentMaker);
                markOnScene = false;
            }
        }
        if (reset && Input.GetKeyDown(KeyCode.R))
        {
            pointP = 0;
            pointE = 0;
            if (setPP >= maxSetPoint || setPE >= maxSetPoint)
            {
                setPP = 0;
                setPE = 0;
            }
            winText.SetActive(false);
            loseText.SetActive(false);
            PointUPdate();
        }
    }
    /// <summary>
    /// ポイントテキスト更新
    /// </summary>
    private void PointUPdate()
    {
        pointTextP.text = pointP.ToString();
        pointTextE.text = pointE.ToString();
        setpointTextP.text = setPP.ToString();
        setpointTextE.text = setPE.ToString();
    }
    /// <summary>
    /// ポイント取得
    /// </summary>
    public void GetPoint()
    {
        shuttle.restop = true;
        _ASGetPoint.PlayOneShot(_ACGetPoint);
        pointP++;
        if (pointP >= maxPoint)
        {
            _ASFinish.PlayOneShot(_ACFinish);
            setPP++;
            pointP = 0;
            pointE = 0;
            if (setPP >= maxSetPoint)
            {
                winText.SetActive(true);
                winText.transform.DOPunchPosition(new Vector3(0,70,0),1f); 
                winText.transform.DOPunchScale(new Vector3(1.2f,1.2f,1.2f),1f);
                reset = true;
            }
        }
        PointUPdate();
    }
    /// <summary>
    /// ポイント取られた
    /// </summary>
    public void LostPoint()
    {
        shuttle.restop = true;
        _ASLostPoint.PlayOneShot(_ACLostPoint);
        pointE++;
        if (pointE >= maxPoint)
        {
            _ASFinish.PlayOneShot(_ACFinish);
            setPE++;
            pointP = 0;
            pointE = 0;
            if (setPE >= maxSetPoint)
            {
                loseText.SetActive(true);
                loseText.transform.DOPunchPosition(new Vector3(0, 70, 0), 1f);
                loseText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
                reset = true;
            }
        }
        PointUPdate();
    }
}
