using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 5f;
    public TMP_Text TimerText;
    public bool isTimeOut = false;

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(gameTime);
    }

    private void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            UpdateTimer(gameTime + 2);
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime -= 1.0f;

        TimerText.text = string.Format("{0:00}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
    }

    void EndGame()
    {
        isTimeOut = true;
        Debug.Log("Time Over!");

        // Logic to stop players
        // Stop all Rigidbodies
        Rigidbody[] allRigidbodies = Object.FindObjectsByType<Rigidbody>(FindObjectsSortMode.None);
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // Disable physics operations
        }

        // Disable all moving scripts
        GameTimer[] players = Object.FindObjectsByType<GameTimer>(FindObjectsSortMode.None);
        foreach (GameTimer player in players)
        {
            player.enabled = false;
        }

        // Freezing the game (but not adjusting the timeScale so that the UI works)
        Time.timeScale = 0f;
    }
}