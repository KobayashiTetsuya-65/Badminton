using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField,Range(0,2)] private int number;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(osu);
        enemy = FindObjectOfType<Enemy>();
    }

    private void osu()
    {
        enemy._Lv = number;
        Debug.Log("ìÔà’ìxí≤êÆ");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
