using UnityEngine;
using UnityEngine.Networking;

public class ServerPanel : WTPanel {

	void Update () {
		if ((Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt)) && (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) && (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && Input.GetKeyDown (KeyCode.S)) {
			ServerStop ();
		}
	}

	public void ServerStop () {
		Main.setServerMode (false);
	}
}
