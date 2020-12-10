using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    private Color clear;
    private Color white;
    private float t = 0f;
    private float fadeSpeed = 0.5f;
    private bool fade = false;

    void Start()
    {
        white = new Color(1f, 1f, 1f, 1f);
        clear = new Color(1f, 1f, 1f, 0f);
    }

    void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = white;
        }
        t = 0f;
        fade = false;
    }

    void Update()
    {
        if (fade == false)
            return;

        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.Lerp(white, clear, t);
        }
        t += Time.deltaTime / fadeSpeed;
        if (t >= 1f) gameObject.SetActive(false);

    }

    public void StartFade()
    {
        fade = true;
    }
}