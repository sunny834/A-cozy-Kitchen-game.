using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class NewInputSystem : MonoBehaviour
{
    private const string Player_Pref_Bindings = "InputBinding";  
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
        if (PlayerPrefs.HasKey(Player_Pref_Bindings))
        {
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Player_Pref_Bindings));
        }
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
            case Binding.Interact:
               return inputActions.PlayerMovement.Interactions.bindings[0].ToDisplayString(); 
            case Binding.Pause:
               return inputActions.PlayerMovement.Pause.bindings[0].ToDisplayString();
                   
        }
    }
    public void RebindBinding(Binding binding,Action OnActionRebound)
    {
        inputActions.PlayerMovement.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch(binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = inputActions.PlayerMovement.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = inputActions.PlayerMovement.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = inputActions.PlayerMovement.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = inputActions.PlayerMovement.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = inputActions.PlayerMovement.Interactions;
                bindingIndex = 0;
                break;
            case Binding.InteractAlt:
                inputAction = inputActions.PlayerMovement.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = inputActions.PlayerMovement.Pause;
                bindingIndex = 0;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
               // Debug.Log(callback.action.bindings[1].path);
                //Debug.Log(callback.action.bindings[1].overridePath);
                callback.Dispose();
                inputActions.PlayerMovement.Enable();
                OnActionRebound();

              //  inputActions.SaveBindingOverridesAsJson();
                PlayerPrefs.SetString(Player_Pref_Bindings, inputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
