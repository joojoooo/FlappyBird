using UnityEngine;

public class MovePipes : MonoBehaviour
{
    public float speed = 1.66614f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
