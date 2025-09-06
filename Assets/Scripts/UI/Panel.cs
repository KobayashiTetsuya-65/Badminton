using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private Image _panel;
    // Start is called before the first frame update
    void Start()
    {
        _panel.DOFade(0f, 2f)
         .SetEase(Ease.Linear)
         .SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
