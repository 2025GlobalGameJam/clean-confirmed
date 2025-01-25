using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float bubbleLifeTime = 10f;

    private Rigidbody targetRigidbody;
    private Collider targetCollider;
    private float timer = 0f;

    public void Initialize(Transform target)
    {
        targetRigidbody = target.GetComponent<Rigidbody>();
        targetCollider = target.GetComponent<Collider>();

        if (targetRigidbody != null)
        {
            targetRigidbody.isKinematic = true;
        }
        if (targetCollider != null)
        {
            targetCollider.enabled = false;
        }

    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the bubble's lifetime
        if (timer >= bubbleLifeTime)
        {
            PopBubble(transform.parent);
        }
    }

    void PopBubble(Transform target)
    {
        if (targetRigidbody != null)
        {
            targetRigidbody.isKinematic = false;
        }
        if (targetCollider != null)
        {
            targetCollider.enabled = true;
        }

        target.SetParent(null);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PopBubble(transform.parent);
    }
}
