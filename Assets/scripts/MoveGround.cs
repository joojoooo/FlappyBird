using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float speed = 1.51f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.x <= -3.050002)
        {
            transform.position = startPosition;
        }
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
