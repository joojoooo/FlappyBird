using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackFade : MonoBehaviour
{
    private Image img;
    private Color clear;
    private Color black;
    private float t = 0f;
    private float fadeSpeed = 0.25f;
    private bool restart = false;
    private bool done = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        img = GetComponentInChildren<Image>();
        black = new Color(0f, 0f, 0f, 1f);
        clear = new Color(0f, 0f, 0f, 0f);
        Setup();
        SceneManager.LoadScene(1);
    }

    void Setup()
    {
        t = 0f;
        restart = false;
        done = false;
        img.enabled = true;
    }

    void Update()
    {
        if (done)
            return;

        if (restart) { img.color = Color.Lerp(clear, black, t); }
        else { img.color = Color.Lerp(black, clear, t); }
        t += Time.deltaTime / fadeSpeed;
        if (t >= 1f)
        {
            img.enabled = false;
            done = true;
            if (restart)
            {
                SceneManager.LoadScene(1);
                Setup();
            }
        }
    }

    public void FadeRestart()
    {
        Setup();
        restart = true;
    }
}