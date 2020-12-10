using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackFade : MonoBehaviour
{
    private Image img;
    private Color clear;
    private Color black;
    private float t = 0f;
    private float fadeSpeed = 0.2f;
    private bool fadeIn = true;
    private bool fadeOut = true;
    private bool fadeInDone = false;
    private bool hang = false;
    public GameManager gameManager;

    void Start()
    {
        img = GetComponent<Image>();
        black = new Color(0f, 0f, 0f, 1f);
        clear = new Color(0f, 0f, 0f, 0f);
    }

    void Update()
    {
        if (hang) return;
        if (fadeIn && !fadeInDone) { img.color = Color.Lerp(clear, black, t); }
        else if (fadeOut) { img.color = Color.Lerp(black, clear, t); }
        t += Time.deltaTime / fadeSpeed;
        if (t >= 1f)
        {
            if ((fadeIn && !fadeOut) || (!fadeIn && fadeOut) || fadeInDone)
            {
                if (fadeIn && !fadeOut)
                {
                    hang = true;
                    gameManager.FadeInDone(this);
                    return;
                }
                Destroy(gameObject);
                return;
            }
            fadeInDone = true;
            t = 0f;
        }
    }

    public void FadeIn(float fadeSpeed = 0.2f)
    {
        this.fadeSpeed = fadeSpeed;
        fadeIn = true;
        fadeOut = false;
        fadeInDone = false;
        t = 0f;
        hang = false;
    }

    public void FadeOut(float fadeSpeed = 0.2f)
    {
        this.fadeSpeed = fadeSpeed;
        fadeIn = false;
        fadeOut = true;
        fadeInDone = true;
        t = 0f;
        hang = false;
    }
}