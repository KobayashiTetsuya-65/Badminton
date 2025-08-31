using System.Collections;
using System.Collections.Generic;
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
    
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(osu);
        enemy = FindObjectOfType<Enemy>();
    }

    private void osu()
    {
        _AS.PlayOneShot(_AC);
        enemy._Lv = number;
        _level.text = (number + 1).ToString();
        Debug.Log("ìÔà’ìxí≤êÆ");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
