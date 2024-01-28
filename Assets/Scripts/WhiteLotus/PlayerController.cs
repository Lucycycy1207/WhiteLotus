using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public HighlightableObject selectedObjectArg;
    }
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;
    //[SerializeField] private Transform mainCameraTranform;
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
    //private float mouseX, mouseY;
    private float moveMultiplier = 1.0f;
    private bool isGrounded;
    private Vector3 playerVelocity;
    [SerializeField] private Transform playerPickTransform;

    //Interaction Raycasts
    private RaycastHit[] hits;

    private bool isWalking;
    Vector3 moveDir;
    private Vector3 lastInteractDir;
    private HighlightableObject selectedObject;
    public bool isPickingSomething;

    //player info:
    private float playerHeight = 2f;
    private float groundHeight = 0f;
    private float playerRadius = 1f;



    private GameObject PickedItem;

    private Transform mainCameraTransform;
    

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
        mainCameraTransform = Camera.main.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Hide Mouse
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        inputManager.OnInteractAction += InputManager_OnInteractAction;

        isPickingSomething = false;
        PickedItem = null;
    }

    public GameObject GetPickedItem()
    {
        return PickedItem;
    }
    public void SetPickedItem(GameObject g)
    {
        PickedItem = g;
        if (g != null)
        {
            isPickingSomething = true;
        }
        else
        {
            isPickingSomething = false;

        }
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (isPickingSomething)
        {
            PickedItem.GetComponent<IPickable>().OnDropped();
        }

        if (selectedObject != null)
        {
            //Debug.Log("not null");
            if (selectedObject.gameObject.TryGetComponent<ISelectable>(out ISelectable isSelectable))
            {
                //Debug.Log("selectable thing");
                isSelectable.OnSelect();
            }
            else if (selectedObject.gameObject.TryGetComponent<IPickable>(out IPickable isPickable))
            {
                if (isPickingSomething == true)
                {
                    isPickable.OnDropped();
                    isPickingSomething = false;
                    PickedItem = null;
                    //Debug.Log("nothing");
                }
                else
                {
                    isPickingSomething = true;

                    //Debug.Log("isPickingSomething");
                    isPickable.OnPicked(playerPickTransform);
                    PickedItem = selectedObject.gameObject;
                }
            }
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

        HandleInteract();
        UpdateMainCamera();

    }


    private void UpdateMainCamera()
    {
        float camera_delay_t = 1f;
        //Debug.Log(mainCameraTransform.position);
        mainCameraTransform.position = Vector3.Lerp(mainCameraTransform.position, this.transform.position, camera_delay_t);
    }
    private void GetInput()
    {
        inputVector = inputManager.GetMovementVectorNormalized();
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");
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
        if (moveDir == Vector3.zero) return;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * turnSpeed);
    }



    private void HandleInteract()
    {
        
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        ///
        //Create 3 ray origin, with height 0, 1, 2
        //Priority Level 0 > 2 > 1
        //if hit something

        //Vector3 groundHeight = transform.position; groundHeight.y = 0;
        //Vector3 tableHeight = transform.position; tableHeight.y = 1;
        //Vector3 shelfHeight = transform.position; shelfHeight.y = 2;
        //RaycastHit[] raycastHit= new RaycastHit[3];
        //Vector3[] HeightList = { groundHeight, shelfHeight, tableHeight };

        //int HeightNum = 3;
        //bool hasHit = false;
        //for (int i = 0; i < HeightNum; i++)
        //{

        //    if (Physics.Raycast(HeightList[i], lastInteractDir, out raycastHit[i], interactDistance, layerMask))
        //    {
        //        if (raycastHit[i].transform.TryGetComponent(out HighlightableObject desk))
        //        {
        //            if (desk != selectedObject)
        //            {
        //                SetSelectedObject(desk);
        //                hasHit = true;
        //                break;
        //            }
        //            else
        //            {
        //                //player still hit to previous selectable game object
        //                hasHit = true;
        //                break;
        //            }
        //        }
        //    }
        //}

        ///


        

        Vector3 playerGround = characterController.transform.position;
        playerGround.y = groundHeight;
        Vector3 playerTop = characterController.transform.position;
        playerTop.y = playerHeight;
        bool hasHit = false;

        hits = Physics.CapsuleCastAll(playerGround, playerTop, playerRadius, lastInteractDir, interactDistance, layerMask);

        if (hits.Length == 0) return;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent(out HighlightableObject interactObject))
            {
                
                if (interactObject != selectedObject)
                    {
                        Debug.Log($"player hits {interactObject.name}");
                        SetSelectedObject(interactObject);
                        hasHit = true;
                        break;
                }
                else
                {
                    //player still hit to previous selectable game object
                    hasHit = true;
                    break;
                }
            }
        }


        if (hasHit == false)
        {
            SetSelectedObject(null);
        }




    }

    private void SetSelectedObject(HighlightableObject _selectedObject)
    {
        this.selectedObject = _selectedObject;

        OnSelectedObjectChanged?.Invoke(this, new OnSelectedObjectChangedEventArgs
        {
            selectedObjectArg = _selectedObject
    });
    }

}
