using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public float MasterVolume { get; private set; } = 1f;
    [SerializeField] AudioSource _ASBgm;
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Debug.Log(_audioSources.Count);
    }
    /// <summary>
    /// AudioSource��List�ɒǉ�
    /// </summary>
    /// <param name="source"></param>
    public void RegisterSource(AudioSource source)
    {
        if (!_audioSources.Contains(source))
        {
            _audioSources.Add(source);
        }
    }
    /// <summary>
    /// BGM�̉��ʒ���
    /// </summary>
    /// <param name="volume"></param>
    public void SetBGMVolume(float volume)
    {
        _ASBgm.volume = volume;
    }
    /// <summary>
    /// AudioSource��List����폜
    /// </summary>
    /// <param name="source"></param>
    public void UnregisterSESource(AudioSource source)
    {
        _audioSources.Remove(source);
    }
    /// <summary>
    /// SE�̉��ʒ���
    /// </summary>
    /// <param name="volume"></param>
    public void SetSEVolume(float volume)
    {
        MasterVolume = volume;
        foreach(var source in _audioSources)
        {
            source.volume = volume;
        }
    }
}
