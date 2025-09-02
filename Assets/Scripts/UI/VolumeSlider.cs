using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        AudioManager.instance.SetBGMVolume(volume);
    }
    public void SetSEVolume(float volume)
    {
        AudioManager.instance.SetSEVolume(volume);
    }
}
