using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public ParticleSystem particle;
    public InputActionAsset map;
    InputActionMap gameplay;
    InputAction movement, startMovement;

    public float movementSpeed = 5f, rotateSpeed = 5f;
    float rotationValue = 0f;
    bool isMoving = false, startFinishSequence = false, gameFinished = false;

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
    void FixedUpdate()
    {
        if (!startFinishSequence)
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
            rotationValue = 0f;
            transform.Rotate(Vector3.up * rotationValue);
        }
        else
        {
            while (transform.position.y < 10 && transform.position.z < 96)
            {
                if(transform.rotation.x < 0.45f)
                    transform.Rotate(Vector3.right * Time.deltaTime);
                transform.Translate(new Vector3(0, 1, 1) * Time.deltaTime * movementSpeed / 10);
                if (transform.position.y > 10)
                   transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                if (transform.position.z > 96)
                   transform.position = new Vector3(transform.position.x, transform.position.y, 96);
            }

                particle.Play();
                
            
                
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            startFinishSequence = true;
           gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
