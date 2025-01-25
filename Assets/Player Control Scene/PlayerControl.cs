using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _moveAction, _rotateAction;

    public float moveSpeed;
    public float rotateSpeed;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _rotateAction = _playerInput.actions.FindAction("Look");
    }


    void Update()
    {
        movePlayer();
        rotatePlayer();
    }

    void movePlayer()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime;
    }

    void rotatePlayer()
    {
        Vector2 rotateValue = _rotateAction.ReadValue<Vector2>();
        rotateValue *= rotateSpeed * Time.deltaTime;
        transform.Rotate(rotateValue.x, 0, rotateValue.y);
    }
}
