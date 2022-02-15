using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    public Rigidbody rb;
    public float walkSpeed = 10f;
    public float jumpHeight = 5f;
    public Transform gameCamera;
    public Vector2 moveInput;
    public Vector3 moveVector;
    void Start()
    {
        if(rb == null)rb = gameObject.GetComponent<Rigidbody>();
        gameCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
