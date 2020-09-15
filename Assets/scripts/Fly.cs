using UnityEngine;

public class Fly : MonoBehaviour
{
    public float velocity = 3.5f;
    public float sinDivider = 100f;
    public float sinSpeed = 8.1954591f;

    private float sinTime = 0;
    private Rigidbody2D rb;
    private float lastJump = 0f;
    private bool release = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetButton("Jump") == true)
        {
            if (release == true && lastJump < Time.time)
            {
                rb.isKinematic = false;
                release = false;
                rb.velocity = Vector2.up * velocity;
                lastJump = Time.time + 0.200f;
            }
        }
        else
        {
            release = true;
        }

        if (rb.isKinematic == true)
        {
            transform.position += Vector3.up * Mathf.Sin(sinTime) / sinDivider;
            sinTime += Time.deltaTime * sinSpeed;
        }
    }
}