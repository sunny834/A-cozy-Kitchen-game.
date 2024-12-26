using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewInputSystem : MonoBehaviour
{
    private InputActions inputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternate;
    private void Awake()
    {
        inputActions= new InputActions();
        inputActions.PlayerMovement.Enable();
        inputActions.PlayerMovement.Interactions.performed += Interactions_performed;
        inputActions.PlayerMovement.InteractAlternate.performed += Interactions_AlternatePerformed;
    }

    private void Interactions_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interactions_AlternatePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementvectorNormalized()
    {
       Vector2 inputVector=inputActions.PlayerMovement.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
       // Debug.Log(inputVector);
        return inputVector;
    }
}
