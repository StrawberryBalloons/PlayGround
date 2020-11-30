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
    public Interactable focus;
    public Vector3 dummyVelocity = new Vector3();

    Vector3 velocity;
    Transform target;
    bool isGrounded;
    public Camera camera;
    public bool paused;




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
        dummyVelocity = move * speed;

        if ((Input.GetButtonDown("Jump") || Input.inputString == " ") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        onLeftMouseClick();
        onRightMouseClick();
    }

    public void onLeftMouseClick()
    { //ON LEFT MOUSE CLICK DO THIS
        if (Input.GetMouseButtonDown(0))
        { //change to 1 for right mouse click
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int range = 20;
            if (Physics.Raycast(ray, out hit, range))
            { //casts ray out until it hits something
                //do thing on hit
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //if not null
                interactable.Interact();
                //else if not in manual
                //attack animation
            }
        }
    }
    public void onRightMouseClick()
    { //ON LEFT MOUSE CLICK DO THIS
        if (Input.GetMouseButtonDown(1))
        { //change to 1 for right mouse click
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int range = 20;
            if (Physics.Raycast(ray, out hit, range))
            { //casts ray out until it hits something
                //do thing on hit
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                { //if you click an interactable
                    Debug.Log(interactable);
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnUnFocus();
            }
            focus = newFocus;
        }
        FollowTarget(newFocus);
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnUnFocus();
        }
        StopFollowTarget();
        focus = null;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; //direction towards target
        Quaternion lookRotation = Quaternion.LookRotation(direction); //rotation to be facing target
        //smooth interpolation towards a direction
        float rotSpeed = 5f;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
    }

    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.interactionTransform;
    }
    public void StopFollowTarget()
    {
        target = null;
    }






}
