using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    public float speed = 20f;
    public float life = 5f;
    public GameObject bubblePrefab;
    public LayerMask whatIsCollidable;

    void Start()
    {
        Destroy(gameObject, life);
    }
     void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if ((whatIsCollidable.value & (1 << collision.gameObject.layer)) > 0)
        {
            CreateBubble(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void CreateBubble(GameObject target)
    {
        GameObject bubble = Instantiate(bubblePrefab, target.transform.position, Quaternion.identity);
        bubble.GetComponent<BubbleScoree>().CaptureTarget(target);
    }
}
