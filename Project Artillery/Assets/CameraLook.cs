using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float XSens = 1f;
    public float YSens = 1f;

    public Transform targetCamera;
    public Transform playerObj;
    public InputManager input;
    float xRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        input.lookEvent += onLook;
    }

    private void OnDisable()
    {
        input.lookEvent -= onLook;
    }

    private void onLook(Vector2 input)
    {
        var inputX = input.x * XSens * Time.deltaTime;
        var inputY = input.y * YSens * Time.deltaTime;

        xRot -= inputY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation =Quaternion.Euler(xRot, 0f, 0f);
        playerObj.Rotate(Vector3.up * inputX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
