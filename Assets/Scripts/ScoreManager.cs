using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;

    public void IncreaseScore (int amount)
    {
        score += amount;
    }
}
