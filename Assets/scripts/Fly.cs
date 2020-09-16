using UnityEngine;

public class Fly : MonoBehaviour
{
    public float velocity = 3.7f;
    public float sinDivider = 100f;
    public float sinSpeed = 8.1954591f;
    public float downRotSpeed = 100f;
    public float upRotSpeed = 350f;

    private Rigidbody2D rb;
    private Quaternion downRot;
    private Quaternion upRot;
    private float lastJump = 0f;
    private bool release = true;
    private float sinTime = 0;

    private AudioSource audioSource;
    private AudioClip point;
    private AudioClip wing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        downRot = Quaternion.Euler(0, 0, -90f);
        upRot = Quaternion.Euler(0, 0, 20f);

        audioSource = GetComponent<AudioSource>();
        point = Resources.Load<AudioClip>("sounds/sfx_point");
        wing = Resources.Load<AudioClip>("sounds/sfx_wing");
    }

    void Update()
    {
        if (Input.GetButton("Jump") == true)
        {
            if (release == true && lastJump < Time.time)
            {
                rb.isKinematic = false;
                release = false;
                audioSource.PlayOneShot(wing);
                lastJump = Time.time + 0.100f;
                rb.velocity = Vector2.up * velocity;
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
        else
        {
            Rotate();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        audioSource.PlayOneShot(point);
    }

    private void Rotate()
    {
        if (rb.velocity.y <= -1)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, downRot, -rb.velocity.y * downRotSpeed * Time.deltaTime);
        }
        else if (rb.velocity.y > 0)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, upRot, rb.velocity.y * upRotSpeed * Time.deltaTime);
        }
    }
}