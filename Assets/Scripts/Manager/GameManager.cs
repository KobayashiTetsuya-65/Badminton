using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [Header("ポイント設定"), SerializeField, Range(1, 21)]
    private int maxPoint;
    private bool reset = false;
    private bool markOnScene = false;
    private int pointP = 0;
    private int pointE = 0;
    private GameObject currentMaker;
    Shuttle shuttle;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        if(shuttle == null)
        {
            shuttle = FindObjectOfType<Shuttle>();
        }
        PointUPdate();
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shuttle.receive && !markOnScene)
        {
            Vector3 receivemarker = new Vector3 (shuttle.randomPos.x, 0f, shuttle.randomPos.z);
            currentMaker = Instantiate(markerPrefab, receivemarker, Quaternion.identity);
            markOnScene = true;
        }
        if (markOnScene)
        {
            if (shuttle.hit)
            {
                Destroy(currentMaker);
                markOnScene= false;
            }
        }
        if (reset && Input.GetKeyDown(KeyCode.R))
        {
            pointP = 0;
            pointE = 0;
            winText.SetActive(false);
            loseText.SetActive(false);
            PointUPdate();
        }
    }
    private void PointUPdate()
    {
        pointTextP.text = pointP.ToString();
        pointTextE.text = pointE.ToString();
    }
    public void GetPoint()//ポイント取得時に呼び出される
    {
        pointP++;
        PointUPdate();
        if (pointP >= maxPoint)
        {
            winText.SetActive(true);
            reset = true;
        }
    }
    public void LostPoint()//ポイント取られた時呼ばれる
    {
        pointE++;
        PointUPdate();
        if(pointE >= maxPoint)
        {
            loseText.SetActive(true);
            reset = true;
        }
    }
}
