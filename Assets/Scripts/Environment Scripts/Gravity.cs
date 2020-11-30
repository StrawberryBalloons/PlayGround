using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 globalGravity = new Vector3(0, -9.8f, 0); //global gravity
    Vector3 localGravity = new Vector3(0, 0, 0); //global gravity
    public GameObject myPlayer;
    public bool onGround = true;
    Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }
    void FixedUpdate(){
        if(!onGround){
        translateGravity(isLocalGravity());
        }
    }
    void translateGravity(Vector3 gravity){
        
            //transform.Translate(Vector3.right * gravity.x * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.up * gravity.y * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.forward * gravity.z * Time.deltaTime, Space.World);
            myRigidbody.velocity += Vector3.right * gravity.x;
            myRigidbody.velocity += Vector3.up * gravity.y;
            myRigidbody.velocity += Vector3.forward * gravity.z;

        
    }
    Vector3 isLocalGravity(){
        if(localGravity.x != 0 || localGravity.y != 0 || localGravity.z != 0){
            return new Vector3(localGravity.x + globalGravity.x, localGravity.y + globalGravity.y,localGravity.z + globalGravity.z);
        }
        return globalGravity;
    }
}
