using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _shootAction;


    public Transform bubbleShoot;
    public GameObject bubblePrefab;
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
            Instantiate(bubblePrefab, bubbleShoot.position, bubbleShoot.rotation);
        }
    }
}
