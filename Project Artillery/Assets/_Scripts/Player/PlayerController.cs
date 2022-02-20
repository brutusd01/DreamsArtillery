using System;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    public Rigidbody rb;
    CapsuleCollider playerCol;
    public float walkSpeed = 10f;
    public float jumpForce = 5f;
    public Transform gameCamera;
    public Vector2 moveInput;
    public bool jumpInput;
    public Vector3 moveVector;
    Vector3 currentVelocity;
    public LayerMask groundMask;

    float distToGround;
    void Awake()
    {
        if(rb == null)rb = gameObject.GetComponent<Rigidbody>();
        playerCol = GetComponent<CapsuleCollider>();
        distToGround = playerCol.bounds.extents.y;
        //if (gameCamera == null) gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }
    void OnEnable()
    {
        _inputManager.moveEvent += onMove;
        _inputManager.jumpEvent += onJump;
        _inputManager.jumpCanceledEvent += onJumpEnd;
        _inputManager.attack1Event += onFire1;
    }
    void Update()
    {
        Grounded();
        CalculateMovement();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
    }
    void Jump()
    {
        if(Grounded() && jumpInput == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        if(playerCol !=null)Gizmos.DrawSphere(new Vector3(rb.position.x, rb.position.y - distToGround, rb.position.z), playerCol.radius);
    }
    bool Grounded()
    {
        return Physics.CheckSphere(new Vector3(rb.position.x, rb.position.y - distToGround, rb.position.z), playerCol.radius, groundMask);
    }
    void OnDisable()
    {
        _inputManager.moveEvent -= onMove;
        _inputManager.jumpEvent -= onJump;
        _inputManager.jumpCanceledEvent -= onJumpEnd;
        _inputManager.attack1Event -= onFire1;
    }
    void onMove(Vector2 value)
    {
        moveInput = value;
    }

    void onJump()
    {
        jumpInput = true;
    }

    void onJumpEnd()
    {
        jumpInput = false;
    }

    void onFire1()
    {
        //Shoot the gun!
        //weapon.doFire(); or something 
    }

    void CalculateMovement()
    {
        Vector3 moveDir = (transform.right.normalized * moveInput.x) + (transform.forward.normalized * moveInput.y);
        Vector3 targetSpeed = moveDir * walkSpeed;
        moveVector = Vector3.SmoothDamp(moveVector, targetSpeed, ref currentVelocity, .15f);

        Jump();
    }
}
