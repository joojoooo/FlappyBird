using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    private float interval = 1.316666f;
    private float maxHeight = 1.75f;
    private float minHeight = -0.71f;

    private float time = 0f;

    void Update()
    {
        if (time < Time.time)
        {
            time = Time.time + interval;
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position += new Vector3(0, Random.Range(minHeight, maxHeight), 0);
        }
    }
}
