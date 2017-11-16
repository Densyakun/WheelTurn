using UnityEngine;
using UnityEngine.UI;

public class MatchingPanel : WTPanel {
	public Button leaveButton;
	public Text mapNameText;
	public Text mapCreatorText;

	void OnEnable () {
	}

	void Update () {
		//TODO Window(Panel)のフォーカス機能を追加し、一番手前に出ているWindowでのみ操作が機能するようにする。そのためにはCanvasに各WindowやPanelをまとめる。
		if (Input.GetKeyDown (KeyCode.Escape)) {
			show (false);
			WTCanvas.titlePanel.show (true);
		}
	}

	void a () {
		//mapNameText.text = interactable ? mapList [sc.n] : "";
		//mapCreatedText.text = interactable ? mapList [sc.n] : "";
	}

	public void b () {
		/*show (false);
		Main.main.StartCoroutine (Main.openMap (MatchingPanel.selectedMap));*/
	}

	public void LeaveButton () {
		show (false);
		WTCanvas.titlePanel.show (true);
	}
}
