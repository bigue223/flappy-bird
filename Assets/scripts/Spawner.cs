using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 1f;
    public GameObject diamantprefab;


    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        float y = Random.Range(minHeight, maxHeight);
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * y;

        // Spawn diamond with a chance (e.g., 50%)
        if (Random.value < 0.5f)
        {
            Vector3 diamondPos = transform.position + Vector3.up * (y + Random.Range(-0.5f, 0.5f));
            diamondPos.z = 0f; // Z n'a pas d'importance en 2D, mais on le remet à 0 pour éviter toute confusion
            GameObject diamond = Instantiate(diamantprefab, diamondPos, Quaternion.identity);

            // Force le diamant devant les pipes
            var sr = diamond.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 10; // Assurez-vous que c'est plus grand que celui des pipes
            }

            Debug.Log("Diamond Z: " + diamond.transform.position.z +
                      ", Sorting Layer: " + (sr ? sr.sortingLayerName : "none") +
                      ", Order in Layer: " + (sr ? sr.sortingOrder.ToString() : "none"));
        }
    }
}

