using UnityEngine;

public class BubblePop : MonoBehaviour
{
    public ParticleSystem popEffect;
    public ScoreFinal scoreTracker;

    private void Start()
    {
        scoreTracker = FindFirstObjectByType<ScoreFinal>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            PopBubble();
            scoreTracker.score += 1;
        }
    }

    void PopBubble()
    {
        Instantiate(popEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}



