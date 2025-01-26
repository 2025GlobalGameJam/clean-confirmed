using UnityEngine;

public class BubblePop : MonoBehaviour
{
    public ParticleSystem popEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            PopBubble();
        }
    }

    void PopBubble()
    {
        Instantiate(popEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}



