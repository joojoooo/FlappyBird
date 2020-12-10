using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private float speed = 1.66614f;

    void Update()
    {
        foreach (Transform child in transform)
        {
            child.position += Vector3.left * speed * Time.deltaTime;
            if (child.position.x <= -14.96f)
            {
                child.position += new Vector3(29.92f, 0, 0);
            }
        }
    }
}
