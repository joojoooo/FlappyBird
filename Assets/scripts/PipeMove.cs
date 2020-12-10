using UnityEngine;

public class PipeMove : MonoBehaviour
{
    private float speed = 1.66614f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= -7f)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyPipe()
    {
        Destroy(gameObject);
    }
}
