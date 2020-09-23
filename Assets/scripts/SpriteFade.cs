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

    void Update()
    {
        if (fade == false)
            return;

        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.Lerp(white, clear, t);
        }
        t += Time.deltaTime / fadeSpeed;
        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public void StartFade()
    {
        fade = true;
    }
}
