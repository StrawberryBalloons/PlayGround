using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{

    public void addShape(int select){
        switch (select)
        {
            case 3:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<MeshFilter>("Shapes/Plane").sharedMesh;
                break;
            case 2:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<MeshFilter>("Shapes/Cube").sharedMesh;
                break;
            case 1:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<MeshFilter>("Shapes/Sphere").sharedMesh;
                break;
            default:
                print("Not an option Error");
                break;
        }
        
    }
}
