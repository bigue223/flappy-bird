using UnityEngine;

public class Diamond : MonoBehaviour
{

    public float speed1 = 5f;
    private float leftEdge1;

    private void Start()
    {
        leftEdge1 = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += speed1 * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge1)
        {
            Destroy(gameObject);
        }
    }

    
}
