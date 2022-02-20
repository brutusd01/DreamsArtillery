using System;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    public Rigidbody rb;
    public float walkSpeed = 10f;
    public float jumpHeight = 5f;
    public Transform gameCamera;
    public Vector2 moveInput;
    public bool jumpInput;
    public Vector3 moveVector;
    Vector3 currentVelocity;
    Vector3 camForward;
    Vector3 camRight;
    void Awake()
    {
        if(rb == null)rb = gameObject.GetComponent<Rigidbody>();
        //if (gameCamera == null) gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void OnEnable()
    {
        _inputManager.moveEvent += onMove;
        _inputManager.jumpEvent += onJump;
        _inputManager.jumpCanceledEvent += onJumpEnd;
        _inputManager.attack1Event += onFire1;
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
        //camForward = gameCamera.forward;
        //camRight = gameCamera.right;

        //camRight.y = camForward.y = 0;
        Vector3 moveDir = (transform.right.normalized * moveInput.x) + (transform.forward.normalized * moveInput.y);
        Vector3 targetSpeed = moveDir * walkSpeed;
        moveVector = Vector3.SmoothDamp(moveVector, targetSpeed, ref currentVelocity, .15f);
    }
    void Update()
    {
        CalculateMovement();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveVector) * Time.fixedDeltaTime);
    }
}
