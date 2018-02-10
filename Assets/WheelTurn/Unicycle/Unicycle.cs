using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Unicycle : NetworkBehaviour {
	public static Unicycle master;

	public WheelCollider wheelCollider;
	public GameObject wheel;

	[SyncVar]
	public string name; //TODO プレイヤー名(仮)
	[SyncVar]
	public int spawnnum;

	void Start () {
		Start0 ();
		Start1 ();
	}

	[Client]
	private void Start0 () {
		if (!isLocalPlayer)
			return;
		
		master = this;
		CameraMover.cameramover.setTarget (transform);
	}

	[Server]
	private void Start1 () {
		spawnnum = Main.getSpawnPointNum ();
		RpcInit (Map.playingmap.mapname, spawnnum);
		Main.spawned (this);
	}

	[Client]
	void Update () {
		wheel.transform.eulerAngles = new Vector3 (wheel.transform.eulerAngles.x + wheelCollider.rpm * 360f * Time.deltaTime / 60f, wheel.transform.eulerAngles.y, wheel.transform.eulerAngles.z);

		if (Input.GetKey (KeyCode.W)) {
			wheelCollider.motorTorque = 250;
		} else if (Input.GetKey (KeyCode.S)) {
			wheelCollider.motorTorque = -250;
		} else {
			wheelCollider.motorTorque = 0;
		}
		if (Input.GetKey (KeyCode.Space)) {
			wheelCollider.brakeTorque = 500;
		} else {
			wheelCollider.brakeTorque = 0;
		}
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb != null) {
			if (Input.GetKey (KeyCode.A)) {
				rb.AddTorque (new Vector3 (0, -1, 0) * (wheelCollider.rpm < 0 ? Mathf.Min (-500, wheelCollider.rpm) : Mathf.Max (500, wheelCollider.rpm)));
			} else if (Input.GetKey (KeyCode.D)) {
				rb.AddTorque (new Vector3 (0, 1, 0) * (wheelCollider.rpm < 0 ? Mathf.Min (-500, wheelCollider.rpm) : Mathf.Max (500, wheelCollider.rpm)));
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

	[ClientRpc]
	public void RpcInit (string mapname, int spawnnum) {
		Map map = Map.openMap (Main.getMap (mapname));
		transform.position = map.getPlayerSpawnPointPosition (spawnnum);
		transform.eulerAngles = map.getPlayerSpawnPointEuler (spawnnum);
	}



	//public GameObject steering;

	//int a = 60;

	/*static float b (float a, float b, float c) {
		if ((c - a) / (b - a) <= 0) {
			return 0;
		}
		if (1 <= (c - a) / (b - a)) {
			return 1;
		}
		return (c - a) / (b - a);
	}*/
}
