using UnityEngine;

public class WTCanvas : MonoBehaviour {
	public static TitlePanel titlePanel;
	public static MatchingPanel MatchingPanel;
	public static SettingPanel settingPanel;
	public static LoadingMapPanel loadingMapPanel;
	public static ErrorQuitPanel errorQuitPanel;
	public static ServerPanel serverPanel;

	void Awake () {
		titlePanel = GetComponentInChildren<TitlePanel> (true);
		MatchingPanel = GetComponentInChildren<MatchingPanel> (true);
		settingPanel = GetComponentInChildren<SettingPanel> (true);
		loadingMapPanel = GetComponentInChildren<LoadingMapPanel> (true);
		errorQuitPanel = GetComponentInChildren<ErrorQuitPanel> (true);
		serverPanel = GetComponentInChildren<ServerPanel> (true);
	}
}
