using UnityEngine;
using UnityEngine.SceneManagement;

public class Fly : MonoBehaviour
{
    public float velocity = 3.5f;
    public float sinDivider = 100f;
    public float sinSpeed = 8.1954591f;
    public float downRotSpeed = 100f;
    public float upRotSpeed = 350f;
    public GameObject pipeSpawner;

    private Rigidbody2D rb;
    private Quaternion downRot;
    private Quaternion upRot;
    private float lastJump = 0f;
    private bool release = true;
    private float sinTime = 0;

    private AudioSource audioSource;
    private AudioClip point;
    private AudioClip wing;
    private AudioClip hit;

    private bool gameOver = false;
    private bool started = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        downRot = Quaternion.Euler(0, 0, -90f);
        upRot = Quaternion.Euler(0, 0, 20f);

        audioSource = GetComponent<AudioSource>();
        point = Resources.Load<AudioClip>("sounds/sfx_point");
        wing = Resources.Load<AudioClip>("sounds/sfx_wing");
        hit = Resources.Load<AudioClip>("sounds/sfx_hit");
    }

    void Update()
    {
        if (gameOver == true)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, downRot, 5f * downRotSpeed * Time.deltaTime);
            if (transform.position.y < -1.8701f)
            {
                rb.isKinematic = true;
                transform.position = new Vector3(transform.position.x, -1.87f, transform.position.z);
            }
            if (Input.GetButton("Jump") == true)
            {
                Invoke("RestartGame", 1f);
            }
            return;
        }

        if (Input.GetButton("Jump") == true)
        {
            if (release == true && lastJump < Time.time)
            {
                if (started == false)
                {
                    started = true;
                    pipeSpawner.SetActive(true);
                    rb.isKinematic = false;
                }
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


        if (started == false)
        {
            transform.position += Vector3.up * Mathf.Sin(sinTime) / sinDivider;
            sinTime += Time.deltaTime * sinSpeed;
        }
        else
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        if (rb.velocity.y <= -2.5f)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, downRot, -rb.velocity.y * downRotSpeed * Time.deltaTime);
        }
        else if (rb.velocity.y > 0f)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, upRot, rb.velocity.y * upRotSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Safe")
        {
            audioSource.PlayOneShot(hit);
            EndGame();
        }
        else
        {
            audioSource.PlayOneShot(point);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Die")
        {
            audioSource.PlayOneShot(hit);
            EndGame();
        }
    }

    private void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            GetComponent<PolygonCollider2D>().enabled = false;

            foreach (MovePipes obj in FindObjectsOfType<MovePipes>())
            {
                obj.enabled = false;
            }
            pipeSpawner.SetActive(false);
            FindObjectOfType<GroundMover>().enabled = false;
        }
    }

    private void RestartGame()
    {
        if (gameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}