using UnityEngine;

public class MovePipes : MonoBehaviour
{
    public float speed = 1.51f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
