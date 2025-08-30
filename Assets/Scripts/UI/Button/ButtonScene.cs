using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonScene : MonoBehaviour
{
    [SerializeField] private Image Fade;
    [SerializeField] string Scenename;
    [SerializeField] private float Fadespeed;
    private bool push = false;
    private float alpha;
    private bool increase;
    private Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(osu);
        _tr = GetComponent<Transform>();
        push = false;
        Fadeset(0);
        Debug.Log("a");
    }
    void Fadeset(float a)
    {
        Fade.color = new Color(0, 0, 0, a);
        alpha = a;
    }
    void Fadeeffect()
    {
        switch (alpha)
        {
            case 0:
                increase = true;
            break;
            case 1:
                increase = false;
            break;
        }
        if(increase)
        {
            alpha += Time.deltaTime * Fadespeed;
            Fade.color = new Color(0,0,0,alpha);
        }
        else
        {
            alpha -= Time.deltaTime * Fadespeed;
            Fade.color = new Color(0, 0, 0, alpha);
        }
    }
    void osu()
    {
        push = true;
        _tr.DOShakeScale(0.3f,0.8f);
        Debug.Log("‰Ÿ‚³‚ê‚½");
    }
    // Update is called once per frame
    void Update()
    {
        if (push)
        {
            Fadeeffect();
            if (alpha <= 0 || alpha >= 1)
            {
                SceneManager.LoadScene(Scenename);
            }
        }
    }
}
