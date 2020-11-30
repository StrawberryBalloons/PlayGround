using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCalculation : MonoBehaviour
{
    public GameObject myObject;
    public Vector3 direction;
    public float area;
    public float mediumDensity;
    public float velocity;
    public float coefficient;
    /*
    *Use rays to get area
    * more detail = more rays that fan out in 360 degrees
    *
    *Calculate angle of deflection
    *
    *Need: a direction, to see if the character is moving (if yes)
    * DragForce=(Fluiddensity)×(Squareofthevelocity)×
    * (Dragcoefficient)×(Cross−sectionarea)
    * Fluid density - density...
    * Drag coefficient - how streamlined it is
    * Cross section area - area of affected area
    * Square of the velocity - speed square
    * Drag coef can be foud with c = 0.01 * angle
    *potentially c = 0.01 * angle -> averaged out between number of samples
    * F = CA(pV^2/2) Force = coef * area * (density * velocity ^2 / 2)


    direction can be transform +/- 1 for xyz and then applied on top of units
    transform xyz to get a direction.
    */
}
