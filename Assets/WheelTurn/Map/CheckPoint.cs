using UnityEngine;
using UnityEngine.Networking;

public class CheckPoint : NetworkBehaviour {
	
	[Server]
	void OnTriggerEnter (Collider other) {
		Unicycle unicycle = other.attachedRigidbody.GetComponent<Unicycle> ();
		if (unicycle != null)
			Main.checkPointPass (this, unicycle);
	}
}
