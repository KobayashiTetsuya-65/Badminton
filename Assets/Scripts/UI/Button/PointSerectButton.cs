using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PointSerectButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Button[] _others;
    [SerializeField] private AudioSource _AS;
    [SerializeField] private AudioClip _AC;
    [SerializeField,Range(1,21)] private int _point;
    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(push);
        AudioManager.instance.RegisterSource(_AS);
        _AS.volume = AudioManager.instance.MasterVolume;
        _button.image.color = Color.white;
    }
    private void OnDestroy()
    {
        AudioManager.instance.UnregisterSESource(_AS);
    }
    private void push()
    {
        _AS.PlayOneShot(_AC);
        AudioManager.instance.Point = _point;
        _button.image.color = new Color(1, 1, 0, 1);
        _button.transform.DOShakeScale(0.3f);
        foreach(var button in _others)
        {
            button.image.color = Color.white;
        }
    }
}
