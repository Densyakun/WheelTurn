using UnityEngine.UI;

public class ErrorQuitPanel : WTPanel {
	public Text errorText;

	new public void show (bool show) {
		base.show (show);
		if (!show)
			setError ("");
	}

	public void setError (string error) {
		errorText.text = error;
	}
}
