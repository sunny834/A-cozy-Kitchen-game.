using System;
using UnityEngine;

public class Player : MonoBehaviour,IkitchenObject
{
    public event EventHandler OnPickSomething;
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float playerRadius = 1f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private NewInputSystem newInputSystem;
    [SerializeField] private LayerMask counterLayerMask;

    private BaseCounter selectedCounter;
    private bool isWalking;
    private Vector3 lastInteractionDir;
    private KitchenObject KitchenObject;
    [SerializeField] private Transform TableTop;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Player instance!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (newInputSystem == null)
        {
            Debug.LogError("NewInputSystem is not assigned in the Inspector.");
            return;
        }
        newInputSystem.OnInteractAction += NewInputSystem_OnInteractAction;
        newInputSystem.OnInteractAlternate += NewInputSystem_OnInteractAlternate;
    }

    private void NewInputSystem_OnInteractAlternate(object sender, EventArgs e)
    {
        if (newInputSystem != null)
        {
            selectedCounter?.InteractAlternate (this);
        }
    }

    private void OnDestroy()
    {
        if (newInputSystem != null)
        {
            newInputSystem.OnInteractAction -= NewInputSystem_OnInteractAction;
        }
    }

    private void Update()
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
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            if (CanMove(moveDirX, moveDistance) && moveDir.x!=0)
            {
                moveDir = moveDirX;
                transform.position += moveDir * moveDistance;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                if (CanMove(moveDirZ, moveDistance) && moveDir.z!=0)
                {
                    moveDir = moveDirZ;
                    transform.position += moveDir * moveDistance;
                }
                else
                {
                    moveDir = Vector3.zero;
                }
            }
        }

        isWalking = moveDir != Vector3.zero;

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = newInputSystem.GetMovementvectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        if (selectedCounter == newSelectedCounter) return;

        selectedCounter = newSelectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = selectedCounter
        });
    }

    private void NewInputSystem_OnInteractAction(object sender, EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return TableTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }
    public void ClearKitchenObject()
    { KitchenObject = null; }
    public bool HaskitchenObject()
    {
        return KitchenObject != null;
    }
}
