using UnityEngine;

public class BubbleScore : MonoBehaviour
{
    public int scoreToGive = 1;
    public int clicksToPop = 3;
    public float scaleIncreasePerClick = 0.1f;
    public ScoreManager scoreManager;


    void OnMouseDown()
    {
        clicksToPop -= 1;
        transform.localScale += Vector3.one * scaleIncreasePerClick;

        if (clicksToPop == 0)
        {
            scoreManager.IncreaseScore(scoreToGive);
            Destroy(gameObject);
        }
    }
    
}
