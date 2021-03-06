﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 jumpForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }

    public void Jump(Vector3 _jumpForce){
        jumpForce = _jumpForce;
    }

    public void Rotate(Vector3 _rotation){
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void FixedUpdate(){
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement() {
        if (velocity != Vector3.zero) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (jumpForce != Vector3.zero)
        {
            rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void PerformRotation(){
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null){
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

}
