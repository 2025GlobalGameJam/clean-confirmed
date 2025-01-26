using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public BubblePop bubblePop;
    private int bubblePoints;


    public void IncreaseScore (int amount)
    {
        score += amount;
    }
    public void BubblePopped()
    {

        if (bubblePoints == 1)
        {
            IncreaseScore(score);
            Destroy(gameObject);
        }
    }
}
