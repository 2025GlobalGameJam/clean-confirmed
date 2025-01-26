using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float bubbleLifeTime = 3f;
    private GameObject capturedObject;
    void Start()
    {
        Destroy(gameObject, bubbleLifeTime);
    }

    public void CaptureTarget(GameObject target)
    {
        capturedObject = target;
        Rigidbody rb = target.GetComponent<Rigidbody>();
        Collider col = target.GetComponent<Collider>();

        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;

        target.transform.SetParent(transform);
    }

    private void OnDestroy()
    {
        if (capturedObject != null)
        {
            Rigidbody rb = capturedObject.GetComponent<Rigidbody>();
            Collider col = capturedObject.GetComponent<Collider>();

            if (rb != null) rb.isKinematic = false;
            if (col != null) col.enabled = true;

            capturedObject.transform.SetParent(null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
