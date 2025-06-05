using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    PlayerInput playerInput;

    public Vector2 moveInput;
    InputAction _moveAction;

    void Awake()
    {
        _moveAction = playerInput.actions["Move"];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = _moveAction.ReadValue<Vector2>();
    }
}
