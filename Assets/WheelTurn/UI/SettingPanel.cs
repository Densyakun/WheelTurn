using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : WTPanel {
	//TODO 多言語対応化
	public static string thankyouText_DEF = "遊んでくれてありがとう！";
	public static string bgmVolumeText_DEF = "BGM音量";
	public static string seVolumeText_DEF = "SE音量";
	public static string dragRotSpeedText_DEF = "マウスドラッグによるカメラ回転速度";
	public Text thankyouText;
	public Text bgmVolumeText;
	public Slider bgmVolumeSlider;
	public Text seVolumeText;
	public Slider seVolumeSlider;

	void Update () {
		thankyouText.text = thankyouText_DEF;
		bgmVolumeText.text = bgmVolumeText_DEF + ": " + (int)(bgmVolumeSlider.value * 100f);
		seVolumeText.text = seVolumeText_DEF + ": " + (int)(seVolumeSlider.value * 100f);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			show (false);
		}
	}

	void load () {
		bgmVolumeSlider.minValue = Main.MIN_BGM_VOLUME;
		bgmVolumeSlider.maxValue = Main.MAX_BGM_VOLUME;
		bgmVolumeSlider.value = Main.bgmVolume;
		seVolumeSlider.minValue = Main.MIN_SE_VOLUME;
		seVolumeSlider.maxValue = Main.MAX_SE_VOLUME;
		seVolumeSlider.value = Main.seVolume;
	}

	new public void show (bool show) {
		if (show) {
			load ();
		}
		base.show (show);
	}

	public void save () {
		Main.bgmVolume = bgmVolumeSlider.value;
		Main.seVolume = seVolumeSlider.value;
		Main.saveSettings ();

		show (false);
	}

	public void reset () {
		//TODO
	}
}
