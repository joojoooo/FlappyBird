using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private Image img;
    private Color clear;
    private Color white;
    private float t = 0f;
    private float speed = 0.5f;

    void Start()
    {
        img = GetComponent<Image>();
        white = new Color(1f, 1f, 1f, 0.9f);
    }

    void Update()
    {
        img.color = Color.Lerp(white, Color.clear, t);
        t += Time.deltaTime / speed;
        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
