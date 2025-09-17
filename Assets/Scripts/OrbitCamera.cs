using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCamera : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float distance = 5f;
    private Vector2 orbitAngles = new Vector2(45f, 0f);
    private Vector2 input;

    bool isCameraBlock = false;
    Vector3 barrier;

    public Transform focus;

   // public InputAction jetPackAction;
    
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnLook(InputValue value)
    {
        input.x = -value.Get<Vector2>().y;
        input.y = value.Get<Vector2>().x;
    }

    bool ManualRotation()
    {
        float deadzone = 0.001f;
        if (input.magnitude > deadzone)
        {
            orbitAngles += input * rotationSpeed * Time.unscaledDeltaTime;
            return true;
        }


        return false;
    }

    void OnZoom(InputValue value)
    {
        distance -= value.Get<Vector2>().y * 0.5f;

        distance = Mathf.Clamp(distance, 0, 10);
    }

    private void LateUpdate()
    {
        Quaternion lookRotation = transform.localRotation;

        if (ManualRotation())
        {
            orbitAngles.x = Mathf.Clamp(orbitAngles.x, -30, 70);
            lookRotation = Quaternion.Euler(orbitAngles);
        }

        Vector3 lookDirection = lookRotation * Vector3.forward;
        //ternary operator      x = (Comparison) ? (when true) : (when false);
        Vector3 lookPosition = isCameraBlock ? barrier : focus.position - lookDirection * distance;

        transform.position = Vector3.Lerp(transform.position, lookPosition, Time.deltaTime * 50f);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50f);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(focus.position, -transform.forward, out hit, distance ))
        {
            isCameraBlock = true;
            barrier = hit.point + hit.normal * 0.2f;
        }
        else
        {
            isCameraBlock = false;
        }
    }
}
