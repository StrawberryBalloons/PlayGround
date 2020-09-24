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
                    setRotation(trackPoint());
                    break;
                case 3://point
                    setRotation(getPointRot());
                    select = -1;
                    break;
                case 2:
                    //Origin & destination
                    Debug.Log("not implemented");
                    select = -1; ;
                    break;
                case 1://target nearest enemy rotation
                    //setRotation(getTargetNearEnemyRot());
                    select = -1;
                    break;
                default:
                    break;
            }
        }
    }
    public Vector3 trackPoint()
    {
        return new Vector3();
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
    public void getCamera(){
        cam = player.transform.Find("playerCamera");
    }
    public void setSelect(int number)
    {
        select = number;
    }
}
