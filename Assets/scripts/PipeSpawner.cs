using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float interval = 1f;
    public GameObject pipe;
    public float maxHeight = 1f;
    public float minHeight = 1f;

    private float time = 0f;

    void Update()
    {
        if (time < Time.time)
        {
            time = Time.time + interval;
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position += new Vector3(0, Random.Range(minHeight, maxHeight), 0);
            Destroy(newPipe, 10);
        }
    }
}
