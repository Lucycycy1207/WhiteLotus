using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedDeskChangedEventArgs> OnSelectedDeskChanged;
    public class OnSelectedDeskChangedEventArgs : EventArgs
    {
        public Desk selectedDesk;
    }
    
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool invertMouse;
    [SerializeField] private float sprintMultiplier = 2;


    [Header("Player Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpVelocity;

    [Header("Player Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Interaction")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float interactionDistance;

    [Header("Manager")]
    [SerializeField] private InputManager inputManager;

    private CharacterController characterController;
    private Vector2 inputVector;
    private float mouseX, mouseY;
    private float moveMultiplier = 1.0f;
    private float camXRotation;
    private bool isGrounded;
    private Vector3 playerVelocity;
    [SerializeField] private ISelectable selection;

    //Interaction Raycasts
    private RaycastHit hit;

    private bool isWalking;
    Vector3 moveDir;
    private Vector3 lastInteractDir;
    private Desk selectedDesk;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputManager.OnInteractAction += InputManager_OnInteractAction;
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {

        if (selectedDesk! != null)
        {
            selectedDesk.Interact();
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        RotatePlayer();

        //GroundCheck();
        MovePlayer();
        //JumpCheck();

        Shoot();
        HandleInteract();
    }

    private void GetInput()
    {
        inputVector = inputManager.GetMovementVectorNormalized();
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1.0f;

    }

    private void MovePlayer()
    {
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;


        characterController.Move(moveDir * moveSpeed * moveMultiplier * Time.deltaTime);

        //Ground Check
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);


    }

    private void RotatePlayer()
    {

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * turnSpeed);
    }


    private void OnDrawGizmos()//give some visual debugging on ground check this case
    {
        Gizmos.color = Color.black;

        Gizmos.DrawSphere(groundCheck.position, groundCheckDistance);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody bullet = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

    }


    private void HandleInteract()
    {
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);


        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        //if hit something
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Desk desk))
            {
                //has desk
                //desk.Interact();
                if (desk != selectedDesk)
                {
                    SetSelectedCounter(desk);
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

    private void SetSelectedCounter(Desk selectedDesk)
    {
        this.selectedDesk = selectedDesk;

        OnSelectedDeskChanged?.Invoke(this, new OnSelectedDeskChangedEventArgs
        {
            selectedDesk = selectedDesk
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
