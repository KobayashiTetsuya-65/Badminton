using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _anker;
    [SerializeField]
    private GameObject _shutle;
    [SerializeField]
    private AudioSource _AS;
    [SerializeField]
    private AudioClip _AC;
    private Vector3 _startPos;
    private Vector3 _prevPos;
    private float _random;
    Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        _tr = GetComponent<Transform>();
        _random = 2f;
        _startPos = _shutle.transform.position;
        StartMove();
        AudioManager.instance.RegisterSource(_AS);
        _AS.volume = AudioManager.instance.MasterVolume;
    }
    private void OnDestroy()
    {
        AudioManager.instance.UnregisterSESource(_AS);
    }
    void StartMove()
    {
        _random = Random.Range(1f, 5f);
        Debug.Log("ˆÚ“®ŽžŠÔ: " + _random);
        float height = 3.0f;
        _prevPos = _startPos;
        DOTween.To(() => 0f, t =>
        {
            //•ú•¨üÀ•W‚ÌŒvŽZ
            Vector3 pos = Vector3.Lerp(_startPos, _anker.transform.position, t);
            float parabola = 4 * t * (1 - t);
            pos.y = Mathf.Lerp(_startPos.y, _anker.transform.position.y, t) + parabola * height;

            _shutle.transform.position = pos;

            //Œü‚«‚ÌŒvŽZ
            Vector3 dir = (pos - _prevPos).normalized;
            if (dir.sqrMagnitude > 0.001f)
            {
                _shutle.transform.rotation = Quaternion.LookRotation(-dir, Vector3.up);
            }
            _prevPos = pos;

        }, 1f, _random)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            _AS.PlayOneShot(_AC);
            _tr.DOPunchScale(new Vector3(1.3f, -1.5f, 0), 1f);
            _shutle.transform.position = _startPos;
            _shutle.transform.rotation = Quaternion.identity;
            StartMove();
        });
    }
}
