using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 30f;
    private bool isGameOver = false;

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(gameTime);
        EndGame();
    }

    void EndGame()
    {
        isGameOver = true;
        Debug.Log("Player failed to escape within the time limit!");

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