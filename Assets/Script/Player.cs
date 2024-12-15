using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; };
    public event EventHandler OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter SelectedCounter;
    }
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float playerRadius = 1f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private NewInputSystem newInputSystem;
    [SerializeField] private LayerMask counterlayerMask;
    private ClearCounter SelectedCounter;
    private bool isWalking;
    private Vector3 lastInteractionsDir;

    private void Awake()
    {
        if(Instance == null)
        {
            Debug.LogError("More than one instance");
        }
        Instance = this;
    }
    private void Start()
    {
        newInputSystem.OnInteractAction += NewInputSystem_OnInteractAction;
    }

    private void NewInputSystem_OnInteractAction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.Interact();
        }
       
    }

    public void Update()
    {
       HandleMovement();
       HandleInteraction();
    }

    private bool CanMove(Vector3 direction, float distance)
    {
        return !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            direction,
            distance
        );
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
         Vector2 inputVector = newInputSystem.GetMovementvectorNormalized();
         Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

     float moveDistance = movementSpeed * Time.deltaTime;

         if (CanMove(moveDir, moveDistance))
          {
        transform.position += moveDir * moveDistance;
          }
             else
         {
        // Try moving only along the X-axis
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        if (CanMove(moveDirX, moveDistance))
        {
            moveDir = moveDirX;
            transform.position += moveDir * moveDistance;
        }
        else
        {
                // Try moving only along the Z-axis
                 Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            if (CanMove(moveDirZ, moveDistance))
            {
                moveDir = moveDirZ;
                transform.position += moveDir * moveDistance;
            }
            else
            {
                // Cannot move in any direction
                moveDir = Vector3.zero;
            }
        }

        }

        // Check if the player is walking
        isWalking = moveDir != Vector3.zero;

        // Rotate towards movement direction if walking
        if (isWalking)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
        }
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = newInputSystem.GetMovementvectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        if (moveDir != Vector3.zero)
        {
            lastInteractionsDir = moveDir;
        }
        float InteractionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionsDir, out RaycastHit raycastHit, InteractionDistance, counterlayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Debug.Log(raycastHit.transform);
                // clearCounter.Interact();
                if (clearCounter != SelectedCounter)
                {
                  SetSelectedCounter(clearCounter);

                   
                }
                
            }
            else
            {
               SetSelectedCounter(null);
            }
           
        }
        else
        {
            //Debug.Log("__");
          SetSelectedCounter(null );
        }
       

    }
    private void SetSelectedCounter(ClearCounter SelectedCounter)
    {
        this.SelectedCounter = SelectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
           
             SelectedCounter = SelectedCounter
            
        });

        
    }
}
