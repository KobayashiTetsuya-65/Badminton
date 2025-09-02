using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField,Range(0,2)] private int number;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private AudioSource _AS;
    [SerializeField] private AudioClip _AC;
    [SerializeField] private Material _markerMat;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(osu);
        enemy = FindObjectOfType<Enemy>();
        _markerMat.color = new Color(1, 0.6f, 0.7f, 0.4f);
        AudioManager.instance.RegisterSource(_AS);
        _AS.volume = AudioManager.instance.MasterVolume;
    }
    private void OnDestroy()
    {
        AudioManager.instance.UnregisterSESource(_AS);
    }
    private void osu()
    {
        _AS.PlayOneShot(_AC);
        enemy._Lv = number;
        _level.text = (number + 1).ToString();
        if(number == 2)
        {
            _markerMat.color = new Color(0, 0, 0, 0);
        }
        else
        {
            _markerMat.color = new Color(1, 0.6f, 0.7f, 0.4f);
        }
            Debug.Log("ìÔà’ìxí≤êÆ");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
