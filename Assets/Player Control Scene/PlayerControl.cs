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

        // Get the camera's forward and right vectors in the local space (XZ plane)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // We want movement to only happen in the XZ plane, so we zero out the Y components
        forward.y = 0f;
        right.y = 0f;

        // Normalize to prevent diagonal movement from being faster
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction based on camera orientation
        Vector3 movement = (forward * direction.y + right * direction.x) * moveSpeed * Time.deltaTime;

        // Apply movement
        transform.position += movement;
    }

    void rotatePlayer()
    {
        Vector3 eulerAngles = cameraTransform.eulerAngles;
        eulerAngles.x = 0f;
        eulerAngles.z = 0f;

        player.rotation = Quaternion.Euler(eulerAngles);
    }
}