using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        AudioManager[] managers = FindObjectsOfType<AudioManager>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
