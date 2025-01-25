using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float lifeTime = 5f;
    public LayerMask whatIsCollidable;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((whatIsCollidable.value & (1 << collision.gameObject.layer)) > 0)
        {
            CreateBubble(collision.transform);
        }
        Destroy(gameObject);
    }

    void CreateBubble(Transform target)
    {
        GameObject bubble = Instantiate(bubblePrefab, target.position, Quaternion.identity);
        bubble.transform.SetParent(target);
        Bubble bubbleScript = bubble.GetComponent<Bubble>();
        bubbleScript.Initialize(target);
    }
}