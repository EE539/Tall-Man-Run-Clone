using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    public InputActionAsset map;
    InputActionMap gameplay;
    InputAction movement, startMovement;

    public float movementSpeed = 5f, rotateSpeed = 5f;
    float rotationValue = 0f;
    bool isMoving = false;

    private void Awake()
    {
        gameplay = map.FindActionMap("Player");
        movement = gameplay.FindAction("Movement");
        startMovement = gameplay.FindAction("StartMovement");

        startMovement.performed += StartMovement_performed;
        movement.performed += Movement_performed;
        startMovement.canceled += StartMovement_canceled;
    }

    private void StartMovement_performed(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        rotationValue = context.ReadValue<float>();
    }

    private void StartMovement_canceled(InputAction.CallbackContext obj)
    {
        rotationValue = 0f;
        isMoving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            //rb.AddForce(transform.forward * movementSpeed); //Move player
            transform.Rotate(Vector3.up * rotationValue);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            if (this.transform.rotation.y > 0.90f)
                transform.rotation = Quaternion.Euler(0, 90f, 0);
            else if (this.transform.rotation.y < -0.90f)
                transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        else
        {
            rotationValue = 0f;
            transform.Rotate(Vector3.up * rotationValue);
        }
        
    }


    private void OnEnable()
    {
        gameplay.Enable();
    }
    private void OnDisable()
    {
        gameplay.Disable();
    }
}
