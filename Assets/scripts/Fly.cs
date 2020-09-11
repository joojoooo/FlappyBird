using UnityEngine;

public class Fly : MonoBehaviour
{
    public float velocity = 2f;
    private Rigidbody2D rb;
    private float lastJump = 0f;
    private bool release = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Jump") == true)
        {
            if (release == true && lastJump < Time.time)
            {
                release = false;
                rb.velocity = Vector2.up * velocity;
                lastJump = Time.time + 0.200f;
            }
        }
        else
        {
            release = true;
        }
    }
}