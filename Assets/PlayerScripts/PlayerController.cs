using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;


    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    //public float strength; //not sure
    //public float vitality; // always 10
    //public float endurance; //how long can click buttons without slowing down
    //public float Mana; //quick maths?
    //public Vector3 velocity;
    //public float mouseSensitivity;

    //[HideInInspector]
    //public Camera camera;
    //public bool canMove = true;
    //float rotX = 0;
    //float rotY = 0;
    //float lookXLimit = 80.0f;
    ////float strafe = 0.7f;
    public bool paused;
    //Rigidbody myRigidbody;


    

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //    myRigidbody = GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{/*
    //*MAKE THREE SPELL WHEELS FOR EACH TYPE OF SPELL (MOTION, STATIC, SHAPE)
    //*/
    //    rotation();
    //}
    //void FixedUpdate(){
    //    move();
    //    velocity = myRigidbody.velocity;
    //}
    //void move(){
    //    if(Input.GetKey(KeyCode.W)){
    //        myRigidbody.velocity += transform.forward * speed;
    //        //transform.forward += speed * Time.deltaTime;
    //    }
    //    if(Input.GetKey(KeyCode.A)){
    //        myRigidbody.velocity += transform.right * -speed;
    //        //transform.right += -speed * Time.deltaTime;
    //    }
    //    if(Input.GetKey(KeyCode.S)){
    //        myRigidbody.velocity += transform.forward * -speed;
    //       //transform.forward += -speed * Time.deltaTime;
    //    }
    //    if(Input.GetKey(KeyCode.D)){
    //        myRigidbody.velocity += transform.right * speed;
    //        //transform.right += speed * Time.deltaTime;
    //    }
    //    if(Input.GetKey(KeyCode.Space) || Input.inputString == " "){
    //        myRigidbody.velocity += transform.up * speed;
    //        //transform.up += strength * Time.deltaTime;
    //    }

    //}
    //void rotation(){ //THIS NEEDS TO BE ALTERED BY GRAVITY
    //    if(canMove && !paused){
    //        rotX += -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    //        rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);
    //        rotY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //        camera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
    //        myRigidbody.rotation = Quaternion.Euler(0, rotY, 0); //x and z need to be gravity direction
    //        //this.transform.rotation = Quaternion.Euler(0, rotY, 0);
    //    }
    //}
}
