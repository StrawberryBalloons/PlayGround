using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    // Start is called before the first frame update
    public int select = -1;
    Rigidbody myRigidbody;
    GameObject player;
    Transform cam;
    public Transform target;
    public float speed = 1;
    bool trig = false;
    Ray ray;
    RaycastHit hitInfo;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select >= 0)
        {
            switch (select)
            {
                case 4://Track
                    trackPoint();
                    break;
                case 3://line
                    setRotation(getPointRot());
                    select = -1;
                    break;
                case 2:
                    //Throw
                    Debug.Log("not implemented");
                    select = -1; ;
                    break;
                case 1://Target Nearest enemy
                    Debug.Log("not implemented");
                    //setRotation(getTargetNearEnemyRot());
                    select = -1;
                    break;
                default:
                    break;
            }
        }
    }
    public void trackPoint()
    {
        if (!trig)
        {
            ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hitInfo, 10))//replace 10 with mana
            {
                target = hitInfo.transform;
                trig = true;
            }
            else
            {
                select = -1;
            }
        }
        else
        {
            target = hitInfo.transform;
        }

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    public Vector3 getTargetNearestEnemyRot()
    {
        return new Vector3();
    }
    public Vector3 getPointRot()
    {
        return new Vector3(
          cam.rotation.eulerAngles.x,
          player.transform.rotation.eulerAngles.y,
          player.transform.rotation.eulerAngles.z);
    }
    public void setRotation(Vector3 rot)
    {
        Debug.Log(rot);
        transform.rotation = Quaternion.Euler(rot);
        Debug.Log(transform.rotation);
    }
    public void getPlayer(GameObject obj)
    {
        player = obj;
        getCamera();
    }
    public void getCamera()
    {
        cam = player.transform.Find("playerCamera");
    }
    public void setSelect(int number)
    {
        select = number;
    }
}
