using TMPro;
using UnityEngine;

public class ScoreFinal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score = 0;

    TMP_Text scoreText;

    private void Update()
    {
        scoreText.text = $"Score: {score}";
    }

}
