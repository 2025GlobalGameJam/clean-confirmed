using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _shootAction;


    public Transform bubbleSpawnPoint;
    public GameObject bubblePrefab;
    public float bubbleSpeed = 10;
    // Update is called once per
    // 
    private void Start()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _shootAction = _playerInput.currentActionMap.FindAction("Attack");
    }
    void Update()
    {
        if (_shootAction.WasPressedThisFrame())
        {
            var bubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, bubbleSpawnPoint.rotation);
            bubble.GetComponent<Rigidbody>().linearVelocity = bubbleSpawnPoint.forward * bubbleSpeed;
        }
    }
}
