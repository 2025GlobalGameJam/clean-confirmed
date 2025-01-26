using UnityEngine;

public class ColliderSpawner : MonoBehaviour
{
    public GameObject objectPrefab;  // Prefab the objects you want to create
    public int spawnCount = 5;      // Number to create

    private BoxCollider boxCollider; // BoxCollider of the current object

    void Start()
    {
        // Get the BoxCollider of the current object
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("This script must be added to any object that has a BoxCollider!", gameObject);
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // Generate random locations within the area of the current box collider
        Vector3 min = boxCollider.bounds.min;
        Vector3 max = boxCollider.bounds.max;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);
        float randomZ = Random.Range(min.z, max.z);

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        // Create a Prefab
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }
}
