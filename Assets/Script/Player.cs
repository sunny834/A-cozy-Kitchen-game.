using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float playerRadius = 1f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private NewInputSystem newInputSystem;
    [SerializeField] private LayerMask counterlayerMask;
    private bool isWalking;
    private Vector3 lastInteractionsDir;

    private void Start()
    {
        newInputSystem.OnInteractAction += NewInputSystem_OnInteractAction;
    }

    private void NewInputSystem_OnInteractAction(object sender, System.EventArgs e)
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
                Debug.Log(raycastHit.transform);
                clearCounter.Interact();
            }
        }
        else
        {
            Debug.Log("__");
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
            }
        }
        else
        {
            //Debug.Log("__");
        }

    }
}
