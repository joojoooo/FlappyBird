using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool startedPlaying = false;
    private bool flying = false;
    private bool jumpBtnReleased = true;
    private float jumpTime = -1f;
    private bool deadByPipe = false;
    private bool dead = false;
    private float deathTime = 0f;
    private float sinTime = 0f;
    private float sinDivider = 100f;
    private float sinSpeed = 8.1954591f;
    private float velocity = 4f;
    private float downRotSpeed = 100f;
    private float upRotSpeed = 350f;
    private int score = 0;
    private AudioSource audioSource;
    private AudioClip pointAudio;
    private AudioClip wingAudio;
    private AudioClip hitAudio;
    private AudioClip dieAudio;
    private Vector3 startPos;
    private Rigidbody2D rb;
    private Quaternion downRot;
    private Quaternion upRot;
    private GameManager gameManager;

    public Animator animator;
    public int birdNumber = 0;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pointAudio = Resources.Load<AudioClip>("sounds/sfx_point");
        wingAudio = Resources.Load<AudioClip>("sounds/sfx_wing");
        hitAudio = Resources.Load<AudioClip>("sounds/sfx_hit");
        dieAudio = Resources.Load<AudioClip>("sounds/sfx_die");

        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        downRot = Quaternion.Euler(0f, 0f, -90f);
        upRot = Quaternion.Euler(0f, 0f, 20f);
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ResetBird()
    {
        startedPlaying = false;
        flying = false;
        jumpBtnReleased = true;
        jumpTime = -1f;
        deadByPipe = false;
        dead = false;
        deathTime = 0f;
        score = 0;
        rb.isKinematic = true;
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rb.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    void Update()
    {
        Rotate();
        if (dead == true)
        {
            if (transform.position.y < -1.8701f)
            {
                rb.isKinematic = true;
                transform.position = new Vector3(transform.position.x, -1.87f, transform.position.z);
            }
            if (deadByPipe == true && Time.time > deathTime + 0.4f)
            {
                audioSource.PlayOneShot(dieAudio);
                deadByPipe = false;
            }
            if (Input.GetButton("Jump" + birdNumber.ToString()) == true && jumpBtnReleased == true && deathTime + 0.3f < Time.time)
            {
                gameManager.RestartGameRequest(birdNumber);
                jumpBtnReleased = false;
            }
            return;
        }

        if (Input.GetButton("Jump" + birdNumber.ToString()) == true)
        {
            if (jumpBtnReleased == true && jumpTime < Time.time)
            {
                if (startedPlaying == false)
                {
                    startedPlaying = true;
                    rb.isKinematic = false;
                    setFlying(true);
                    gameManager.StartGame(birdNumber);
                }
                jumpBtnReleased = false;
                audioSource.PlayOneShot(wingAudio);
                jumpTime = Time.time + 0.050f;
                rb.velocity = Vector2.up * velocity;
            }
        }
        else { jumpBtnReleased = true; }

        if (!startedPlaying)
        {
            transform.position += Vector3.up * Mathf.Sin(sinTime) / sinDivider;
            sinTime += Time.deltaTime * sinSpeed;
            return;
        }
    }

    private void Rotate()
    {
        if (!startedPlaying) return;
        if (rb.velocity.y <= -2.5f)
        {
            setFlying(false);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, downRot, -rb.velocity.y * downRotSpeed * Time.deltaTime);
        }
        else if (rb.velocity.y > 0f)
        {
            setFlying(true);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, upRot, rb.velocity.y * upRotSpeed * Time.deltaTime);
        }
        else if (dead)
        {
            setFlying(false);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, downRot, 4 * downRotSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Point")
        {
            audioSource.PlayOneShot(pointAudio);
            score++;
            gameManager.SetScore(birdNumber, score);
        }
        else
        {
            if (col.tag == "Pipe")
                deadByPipe = true;
            Die();
        }
    }

    private void Die()
    {
        if (dead) return;
        dead = true;

        jumpBtnReleased = true;
        deathTime = Time.time;
        GetComponent<CircleCollider2D>().enabled = false;
        audioSource.PlayOneShot(hitAudio);
        gameManager.birdDeath(birdNumber);
    }

    private void setFlying(bool nflying)
    {
        if (nflying != flying)
        {
            flying = nflying;
            animator.SetBool("Flying", flying);
        }
    }
}