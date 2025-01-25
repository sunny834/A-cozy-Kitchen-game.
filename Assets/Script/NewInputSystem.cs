using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class NewInputSystem : MonoBehaviour
{
    public static NewInputSystem Instance {  get; private set; }
    private InputActions inputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseAction;

    public enum Binding{
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        InteractAlt,
        Pause,
    }
    private void Awake()
    {
        Instance = this;
        inputActions= new InputActions();
        inputActions.PlayerMovement.Enable();
        inputActions.PlayerMovement.Interactions.performed += Interactions_performed;
        inputActions.PlayerMovement.InteractAlternate.performed += Interactions_AlternatePerformed;
        inputActions.PlayerMovement.Pause.performed += Pause_performed;

        Debug.Log(GetBindingText(Binding.Move_Up));
    }

    private void OnDestroy()
    {
        inputActions.PlayerMovement.Interactions.performed -= Interactions_performed;
        inputActions.PlayerMovement.InteractAlternate.performed -= Interactions_AlternatePerformed;
        inputActions.PlayerMovement.Pause.performed -= Pause_performed;
        inputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       OnPauseAction?.Invoke(this,EventArgs.Empty);
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

    public string GetBindingText(Binding binding)
    {
        switch (binding) {
            default:
            case Binding.Move_Up:
               return inputActions.PlayerMovement.Move.bindings[1].ToDisplayString();            
            case Binding.Move_Down:
                return inputActions.PlayerMovement.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return inputActions.PlayerMovement.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return inputActions.PlayerMovement.Move.bindings[4].ToDisplayString();
            case Binding.InteractAlt:
               return inputActions.PlayerMovement.InteractAlternate.bindings[0].ToDisplayString(); 
            case Binding.Pause:
               return inputActions.PlayerMovement.Pause.bindings[0].ToDisplayString();
                   
        }
    }
    public void RebindBinding(Binding binding,Action OnActionRebound)
    {
        inputActions.PlayerMovement.Disable();
        inputActions.PlayerMovement.Move.PerformInteractiveRebinding(1)
            .OnComplete(callback =>
            {
                Debug.Log(callback.action.bindings[1].path);
                Debug.Log(callback.action.bindings[1].overridePath);
                callback.Dispose();
                inputActions.PlayerMovement.Enable();
                OnActionRebound();
            })
            .Start();
    }
}
