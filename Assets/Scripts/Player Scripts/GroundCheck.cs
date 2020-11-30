using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    Gravity myGravity;
    public float rayDistance = 10;
    void Start()
    {
       myGravity = GetComponent<Gravity>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayDistance)){
            Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
            myGravity.onGround = true;
        }else{
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);
            myGravity.onGround = false;
        }
    }
}
