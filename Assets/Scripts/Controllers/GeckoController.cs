using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoController: MonoBehaviour {
    [SerializeField] Transform target;

    	[Header("Head Tracking")]
    [SerializeField] Transform headBone;
    [SerializeField] float headMaxTurnAngle = 70f;
    [SerializeField] float headTrackingSpeed = 10f;

	    [Header("Eye Tracking")]
    [SerializeField] Transform leftEyeBone;
    [SerializeField] Transform rightEyeBone;

    [SerializeField] float eyeTrackingSpeed;
    [SerializeField] float leftEyeMaxYRotation;
    [SerializeField] float leftEyeMinYRotation;
    [SerializeField] float rightEyeMaxYRotation;
    [SerializeField] float rightEyeMinYRotation;

  void LateUpdate()
  {
    HeadTrackingUpdate();
    EyeTrackingUpdate();
  }
      
  void HeadTrackingUpdate()
  {
        // Store the current head rotation since we will be resetting it
        Quaternion currentLocalRotation = headBone.localRotation;
        // Reset the head rotation so our world to local space transformation will use the head's zero rotation. 
        // Note: Quaternion.Identity is the quaternion equivalent of "zero"
        headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = target.position - headBone.position;
        Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

        // Apply angle limit
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle, // Note we multiply by Mathf.Deg2Rad here to convert degrees to radians
            0 // We don't care about the length here, so we leave it at zero
        );

        // Get the local rotation by using LookRotation on a local directional vector
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        // Apply smoothing
        // More info: http://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/
		headBone.localRotation = Xslerp(headBone.localRotation, currentLocalRotation, targetLocalRotation, headTrackingSpeed);
  }

  Quaternion Xslerp(Quaternion boneRot,Quaternion currentRot,Quaternion targetRot,float trackSpeed){
	  boneRot = Quaternion.Slerp(
            currentRot,
            targetRot,
            1 - Mathf.Exp(-trackSpeed * Time.deltaTime)
        );
		return boneRot;
  }
  
  void EyeTrackingUpdate()
  {
        // We use head position here just because the gecko doesn't look so great when cross eyed.
        // Other models may want to split this and use directions relative to the eye origin itself

        Quaternion targetEyeRotation = Quaternion.LookRotation(
            target.position - headBone.position, // toward target
            transform.up
        );

		leftEyeBone.rotation = Xslerp(leftEyeBone.rotation, leftEyeBone.rotation, targetEyeRotation, eyeTrackingSpeed);

		rightEyeBone.rotation = Xslerp(rightEyeBone.rotation, rightEyeBone.rotation, targetEyeRotation, eyeTrackingSpeed);

        // Apply angular limits
        // Ensure the Y rotation is in the range -180 ~ 180

        float leftEyeCurrentYRotation = leftEyeBone.localEulerAngles.y;
        float rightEyeCurrentYRotation = rightEyeBone.localEulerAngles.y;

        // Move the rotation to a -180 ~ 180 range
        if (leftEyeCurrentYRotation > 180)
        {
            leftEyeCurrentYRotation -= 360;
        }
        if (rightEyeCurrentYRotation > 180)
        {
            rightEyeCurrentYRotation -= 360;
        }

        // Clamp the Y axis rotation
        float leftEyeClampedYRotation = Mathf.Clamp(
            leftEyeCurrentYRotation,
            leftEyeMinYRotation,
            leftEyeMaxYRotation
        );
        float rightEyeClampedYRotation = Mathf.Clamp(
            rightEyeCurrentYRotation,
            rightEyeMinYRotation,
            rightEyeMaxYRotation
        );

        // Apply the clamped Y rotation without changing the X and Z rotations
        leftEyeBone.localEulerAngles = new Vector3(
            leftEyeBone.localEulerAngles.x,
            leftEyeClampedYRotation,
            leftEyeBone.localEulerAngles.z
        );
        rightEyeBone.localEulerAngles = new Vector3(
            rightEyeBone.localEulerAngles.x,
            rightEyeClampedYRotation,
            rightEyeBone.localEulerAngles.z
        );
  }
}