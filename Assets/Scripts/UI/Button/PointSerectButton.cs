using UnityEngine;
using UnityEngine.UI;

public class PointSerectButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _AS;
    [SerializeField] private AudioClip _AC;
    [SerializeField,Range(1,21)] private int _point;
    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(push);
        AudioManager.instance.RegisterSource(_AS);
        _AS.volume = AudioManager.instance.MasterVolume;
    }
    private void OnDestroy()
    {
        AudioManager.instance.UnregisterSESource(_AS);
    }
    private void push()
    {
        _AS.PlayOneShot(_AC);
        AudioManager.instance.Point = _point;
    }
}
