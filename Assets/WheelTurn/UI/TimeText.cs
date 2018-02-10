using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {
	Text text;

	void OnEnable () {
		text = GetComponent<Text> ();
	}

	void Update () {
		if (Map.playingmap == null) {
			//text.text = "---";
			text.text = "TimeText.csを修正して下さい";
		} else {
			//TODO ラップタイム等
			/*text.text = "現在時刻: " + (Main.playingmap.getDays () + 1) + "日目 " +
			Main.playingmap.getHours () + ":" + Main.playingmap.getMinutes () +
			":" + Main.playingmap.getSeconds ();*/
		}
	}
}
