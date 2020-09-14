using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float speed = 1.66614f;
    public float loopPos = -3.038f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= loopPos)
        {
            transform.position = startPosition + Vector3.left * speed * Time.deltaTime;
        }
    }
}
