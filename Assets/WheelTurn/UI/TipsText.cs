﻿using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour {
	public static string[] tips = new string[] { "メニューはEscキーで戻ることが出来ます", "ポーズメニューはEscキーで開きます", "F2でスクリーンショットを撮ることが出来ます", "保存したスクリーンショットは設定から見ることが出来ます" };
	//TODO Tipsを定期的に更新する

	public static float changeIntervalperChar = 0.4f;
	int tipsIndex = 0;
	float nextUpdate = 0;

	void OnEnable () {
		tipsIndex = UnityEngine.Random.Range (0, tips.Length);
	}

	void FixedUpdate () {
		if (nextUpdate <= Time.time) {
			changeTips ();
			nextUpdate = Time.time + changeIntervalperChar * tips [tipsIndex].Length;
		}
	}

	public void changeTips () {
		if ((tipsIndex += 1) >= tips.Length) {
			tipsIndex = 0;
		}
		GetComponent<Text> ().text = "Tips: " + tips [tipsIndex];
	}
}
