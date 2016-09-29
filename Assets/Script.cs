using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class Script : NetworkBehaviour {
	public Camera camera;
	public WheelCollider wheel;
	//public GameObject steering;
	//int a = 60;
	void Start () {
	}
	void Update () {
        if (isLocalPlayer)
        {
            if (camera != null)
            {
                camera.enabled = true;
                camera.transform.localPosition -= camera.transform.localPosition * Time.deltaTime * 5;
                Vector3 rot = camera.transform.localEulerAngles;
                if (rot.x >= 180)
                {
                    rot.x -= (rot.x - 360) * Time.deltaTime * 5;
                }
                else
                {
                    rot.x -= rot.x * Time.deltaTime * 5;
                }
                if (rot.y >= 180)
                {
                    rot.y -= (rot.y - 360) * Time.deltaTime * 5;
                }
                else
                {
                    rot.y -= rot.y * Time.deltaTime * 5;
                }
                if (rot.z >= 180)
                {
                    rot.z -= (rot.z - 360) * Time.deltaTime * 5;
                }
                else
                {
                    rot.z -= rot.z * Time.deltaTime * 5;
                }
                camera.transform.localEulerAngles = rot;
            }
            if (Input.GetKey(KeyCode.W))
            {
                wheel.motorTorque = 250;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                wheel.motorTorque = -250;
            }
            else
            {
                wheel.motorTorque = 0;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                wheel.brakeTorque = 500;
            } else
            {
                wheel.brakeTorque = 0;
            }
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddTorque(new Vector3(0, -1, 0) * (wheel.rpm < 0 ? Mathf.Min(-500, wheel.rpm) : Mathf.Max(500, wheel.rpm)));
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    rb.AddTorque(new Vector3(0, 1, 0) * (wheel.rpm < 0 ? Mathf.Min(-500, wheel.rpm) : Mathf.Max(500, wheel.rpm)));
                }
            }
        }
        //steering.transform.position = wheel.transform.position;
        //steering.transform.rotation = new Quaternion(0, steering.transform.rotation.y, steering.transform.rotation.z, steering.transform.rotation.w);
        /*if (a == 0) {
			wheel.GetComponent<Rigidbody>().isKinematic = false;
			steering.GetComponent<Rigidbody>().isKinematic = false;
			if (Input.GetKeyDown(KeyCode.R)) {
				a = 60 * 3;
				wheel.GetComponent<Rigidbody>().velocity = new Vector3();
				wheel.transform.rotation = new Quaternion();
				wheel.transform.localPosition = new Vector3(wheel.transform.localPosition.x, wheel.transform.localPosition.y + 1, wheel.transform.localPosition.z);
				steering.transform.position = wheel.transform.position;
				steering.GetComponent<Rigidbody>().velocity = new Vector3();
				steering.transform.rotation = new Quaternion();
			} else {
				if (Input.GetKey(KeyCode.W)) {
					wheel.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 4);
				}
				if (Input.GetKey(KeyCode.S)) {
					wheel.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.left * 4);
				}
				if (Input.GetKey(KeyCode.A)) {
					steering.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * 4);
				} else if (Input.GetKey(KeyCode.Q)) {
					steering.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * 4);
				}
				if (Input.GetKey(KeyCode.D)) {
					steering.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.back * 4);
				} else if (Input.GetKey(KeyCode.E)) {
					steering.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * 4);
				}
			}
		} else {
			wheel.GetComponent<Rigidbody>().isKinematic = false;
			steering.GetComponent<Rigidbody>().isKinematic = false;
			if (a < 0) {
				a = 0;
			} else {
				a--;
			}
		}*/
    }
	/*static float b(float a, float b, float c) {
		if ((c - a) / (b - a) <= 0) {
			return 0;
		}
		if (1 <= (c - a) / (b - a)) {
			return 1;
		}
		return (c - a) / (b - a);
	}*/
}
