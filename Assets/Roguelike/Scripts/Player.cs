using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool _moveKeyHeld;
    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Movement.started += OnMovement;
        _controls.Player.Movement.canceled += OnMovement;
        _controls.Player.Exit.performed += OnExit;
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Movement.started -= OnMovement;
        _controls.Player.Movement.canceled -= OnMovement;
        _controls.Player.Exit.performed -= OnExit;
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _moveKeyHeld = true;
        else if(ctx.canceled) _moveKeyHeld = false;
    }
    
    private void OnExit(InputAction.CallbackContext obj) => Debug.Log("Exit");

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPlayerTurn && _moveKeyHeld) MovePlayer();
    }

    private void MovePlayer()
    {
        transform.position += (Vector3)_controls.Player.Movement.ReadValue<Vector2>();
        GameManager.Instance.EndTurn();
    }
}
