using UnityEngine;

public class WTPanel : MonoBehaviour {
	
	public void show (bool show) {
		gameObject.SetActive (show);
	}

	public bool isShowing () {
		return gameObject.activeInHierarchy;
	}
}
