using UnityEngine;

public class TitlePanel : WTPanel {
	
	void Update () {
		if ((Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt)) && (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) && (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && Input.GetKeyDown (KeyCode.S)) {
			Main.setServerMode (true);
		}
	}

	public void quit () {
		Main.quit ();
	}

	public void joinGame () {
		show (false);
		Main.main.StartCoroutine (Main.joinGame ());
	}
}
