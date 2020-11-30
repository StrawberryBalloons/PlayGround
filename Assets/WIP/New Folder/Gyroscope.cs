using UnityEngine;

public class Gyroscope: MonoBehaviour {

	private void Start() {

		InputSystem.EnableDevice(Gyroscope.current);
		if (Gyroscope.current.enabled){
             Debug.Log("Gyroscope is enabled");
        }
        // Get sampling frequency of gyro.
var frequency = Gyroscope.current.samplingFrequency;

// Set sampling frequency of gyro to sample 16 times per second.
Gyroscope.current.samplingFrequency = 16;

	}

	// Update is called once per frame
	void Update() {
		transform.rotation = GyroToUnity(Gyroscope.current.attitude);
	}

	private Quaternion GyroToUnity(Quaternion q) {
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}
}