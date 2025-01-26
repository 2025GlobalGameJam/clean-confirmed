using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnAround : MonoBehaviour
{
    PlayerInputManager inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    public GameObject[] Spawns;
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputManager.
    }
}
