using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMaster : MonoBehaviour
{
    [SerializeField] private Button myButton;
    [SerializeField] private RectTransform parentButton;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private RectTransform[] uiElements;
    [Header("ŠÔŠu"), SerializeField, Range(0f, 2f)] private float duration = 0.5f;
    [SerializeField] private AudioSource _AS;
    [SerializeField] private AudioClip _AC;
    private float number = 0;
    private float n = 0;
    private bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(osu);
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        AudioManager.instance.RegisterSource(_AS);
        _AS.volume = AudioManager.instance.MasterVolume;
    }
    private void OnDestroy()
    {
        AudioManager.instance.UnregisterSESource(_AS);
    }
    private void osu()
    {
        if (!change)
        {
            _AS.PlayOneShot(_AC);
            change = true;
            if (n % 2 == 0)
            {
                foreach (GameObject btn in buttons)
                {
                    btn.SetActive(true);
                }
                foreach (RectTransform rect in uiElements)
                {
                    number += -28f;
                    StartCoroutine(ShowButton(rect, new Vector2(0f, number)));
                }
            }
            if (n % 2 == 1)
            {
                foreach (RectTransform rect in uiElements)
                {
                    StartCoroutine(ShowButton(rect, new Vector2(0f, 0f)));
                }
                StartCoroutine(Wait());
            }
            n++;
            number = 0;
        }
    }
    IEnumerator ShowButton(RectTransform rect, Vector2 targetPos)
    {
        Vector2 start = rect.anchoredPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rect.anchoredPosition = Vector2.Lerp(start, targetPos, t);
            yield return null;
        }

        rect.anchoredPosition = targetPos;
        change = false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
