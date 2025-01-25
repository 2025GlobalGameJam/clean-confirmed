using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _lookAction;

    public float cameraSpeed;


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _lookAction = _playerInput.actions.FindAction("Look");
    }

    void Update()
    {
        
    }

    void moveCamera()
    {
        Vector2 direction = _lookAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * cameraSpeed * Time.deltaTime;
    }
}
