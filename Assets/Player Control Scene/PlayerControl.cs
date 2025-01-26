using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _moveAction;

    public float moveSpeed;

    public Transform cameraTransform;
    public Transform player;

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
    }

    void Update()
    {
        movePlayer();
        rotatePlayer();
        
    }

    void movePlayer()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();

        // Calculate the camera's forward and right vectors
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // We only want horizontal movement along the XZ plane, so zero out the Y components
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors to avoid accelerated diagonal movement
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction based on camera orientation
        Vector3 movement = (forward * direction.y + right * direction.x) * moveSpeed * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.MovePosition(transform.position + movement);
        }
        else
        {
            transform.position += movement;
        }

    }

    void rotatePlayer()
    {
        Vector3 eulerAngles = cameraTransform.eulerAngles;
        eulerAngles.x = 0f; 
        eulerAngles.z = 0f; 

        player.rotation = Quaternion.Euler(eulerAngles);
    }
}
