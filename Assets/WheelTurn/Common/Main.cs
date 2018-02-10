using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Main : NetworkBehaviour {
	public const string VERSION = "0.001alpha";

	public const string KEY_FIRSTSTART = "FIRSTSTART";
	public const string KEY_SETUPPED = "SETUPPED";
	public const string KEY_BGM_VOLUME = "BGM_VOL";
	public const string KEY_SE_VOLUME = "SE_VOL";
	private const string MATCHING_SERVER_ADDR = "http://wheelturn-matchingserver-densyakun.c9users.io/match.php";
	//private const string MATCHING_SERVER_ADDR = "http://gm.crazywolf.ml/wt/match.php";
	//TODO マッチングサーバーからIP及びポート番号が送られる
	private const int SERVER_PORT_MIN = 50100;
	private const int SERVER_PORT_MAX = 50199;
	public const int MAX_PLAYERS = 8;
	private const float WWWTimeOut = 3.0f;
	private const bool LOCAL_MODE = true;

	public const float MIN_BGM_VOLUME = 0f;
	public const float MAX_BGM_VOLUME = 1f;
	public const float DEFAULT_BGM_VOLUME = 1f / 3;
	public const float MIN_SE_VOLUME = 0f;
	public const float MAX_SE_VOLUME = 1f;
	public const float DEFAULT_SE_VOLUME = 1f;

	public static Main main;
	public static bool SERVER_MODE = false;
	public enum GameState
	{
		WAITING, RACING, ENDED
	}
	public static GameState state = GameState.ENDED;
	public static IDictionary<int, Unicycle> _spawned = new Dictionary<int, Unicycle> (MAX_PLAYERS);

	private static bool firstStart = false;
	public static bool isFirstStart {
		get { return firstStart; }
		private set { firstStart = value; }
	}
	public static DateTime[] firstStartTimes { get; private set; }
	public static bool isSetupped = false;
	public static float bgmVolume = DEFAULT_BGM_VOLUME;
	public static float seVolume = DEFAULT_SE_VOLUME;

	public Camera mainCamera; //メインカメラ
	public AudioClip[] titleClips;
	public AudioSource bgmSource;
	public AudioSource seSource;
	public Map[] maps;

	//TODO マップの一部をランダムにすることで更に面白く出来る

	void Awake () {
		Main.main = this;

		//初期設定を行っているかどうか
		isSetupped = PlayerPrefs.GetInt(KEY_SETUPPED, 0) == 1;
	}

	void Start () {
		/*if (isSetupped) {
			title ();
		} else {
			//TODO 初期設定
		}*/

		title ();
	}

	void Update () {
		if (SERVER_MODE)
			return;
		
		/*if (playingmap != null) {
			if (bgmSource.isPlaying)
				bgmSource.Stop ();
			
		} else if (!bgmSource.isPlaying) {
			bgmSource.clip = titleClips [UnityEngine.Random.Range (0, titleClips.Length)];
			bgmSource.Play ();
		}*/
	}

	public static void quit () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif !UNITY_WEBPLAYER
		Application.Quit ();
		#endif
	}

	public static void errorQuit (string error) {
		WTCanvas.errorQuitPanel.setError (error);
		WTCanvas.errorQuitPanel.show (true);
	}

	public static void saveSettings () {
		PlayerPrefs.SetFloat (KEY_BGM_VOLUME, main.bgmSource.volume = bgmVolume);
		PlayerPrefs.SetFloat (KEY_SE_VOLUME, main.seSource.volume = seVolume);
	}



	public static void title () {
		Map.closeMap ();

		WTCanvas.serverPanel.show (false);
		WTCanvas.titlePanel.show (true);
	}

	//サーバー操作

	public static bool startServer () {
		if (LOCAL_MODE) {
			NetworkManager.singleton.networkPort = SERVER_PORT_MIN;
			NetworkManager.singleton.StartServer ();
		} else {
			for (int a = SERVER_PORT_MIN; a <= SERVER_PORT_MAX; a++) {
				NetworkManager.singleton.networkPort = a;
				//TODO サーバーが開けない理由がポートを使用中でない場合負担がかかるため対策をする
				if (NetworkManager.singleton.StartServer ())
					break;
			}
		}

		if (NetworkManager.singleton.isNetworkActive)
			return true;
		else
			errorQuit ("ポートが埋まっているか、サーバーが開けませんでした");
		
		return false;
	}

	public static void setServerMode (bool serverMode) {
		if (serverMode == Main.SERVER_MODE)
			return;
		
		if (Main.SERVER_MODE = serverMode) {
			if (startServer ()) {
				WTCanvas.titlePanel.show (false);
				WTCanvas.serverPanel.show (true);
			}
		} else {
			WTCanvas.serverPanel.show (false);
			NetworkManager.singleton.StopServer ();
		}
	}

	//マッチングサーバー接続

	public static IEnumerator matchPost (WWWForm form) {
		WWW result = new WWW (MATCHING_SERVER_ADDR, form);
		float endTime = Time.realtimeSinceStartup + WWWTimeOut;

		while (!result.isDone) {
			if (Time.realtimeSinceStartup < endTime)
				yield return null;
			else {
				result.Dispose ();
				break;
			}
		}

		yield return result;
	}

	public static IEnumerator joinGame () {
		if (LOCAL_MODE) {
			NetworkManager.singleton.networkAddress = "localhost";
			NetworkManager.singleton.networkPort = SERVER_PORT_MIN;
			NetworkManager.singleton.StartClient ();
		} else {
			WWWForm form = new WWWForm ();
			form.AddField ("version", VERSION);
			IEnumerator coroutine = matchPost (form);
			yield return main.StartCoroutine (coroutine);
			if (coroutine is WWW) {
				WWW result = (WWW)coroutine.Current;
				if (!string.IsNullOrEmpty (result.error)) {
					errorQuit (result.error);
					yield break;
				}

				string json = result.text;
				object obj = JsonUtility.FromJson<object> (json);
				if (obj is Json_Text) {
					string text = ((Json_Text)obj).text;
					if (text == JsonData.UNSUPPORTED) {
						//TODO アプリ更新を推薦させる

						errorQuit ("UNSUPPORTED");
						yield break;
					}
				} else if (obj is Json_ADDR_AND_PORT) {
					NetworkManager.singleton.networkAddress = ((Json_ADDR_AND_PORT)obj).addr;
					NetworkManager.singleton.networkPort = ((Json_ADDR_AND_PORT)obj).port;
					NetworkManager.singleton.StartClient ();
				} else {
					print ("return: " + result.text);
				}
			}
		}
	}

	private static IEnumerator sendHosting () {
		if (!LOCAL_MODE) {
			WWWForm form = new WWWForm ();
			form.AddField ("players", NetworkManager.singleton.numPlayers);
			form.AddField ("port", NetworkManager.singleton.networkPort);
			IEnumerator coroutine = matchPost (form);
			yield return main.StartCoroutine (coroutine);
			if (coroutine is WWW) {
				WWW result = (WWW)coroutine.Current;
				if (!string.IsNullOrEmpty (result.error)) {
					errorQuit (result.error);
					yield break;
				}
			}
		}
	}

	public static Map getMap (string mapname) {
		foreach (Map m in main.maps) {
			if (m.mapname == mapname) {
				return m;
			}
		}
		return null;
	}

	public static Map openMap () {
		return Map.openMap (main.maps [UnityEngine.Random.Range (0, main.maps.Length)]);
	}



	public static void waitGame () {
		state = GameState.WAITING;
		print ("waitGame");

		Main.openMap ();
		main.StartCoroutine (sendHosting ());
	}

	public static void startRace () {
		state = GameState.RACING;
		print ("startRace");

		main.StartCoroutine (sendHosting ());
	}

	public static void endRace () {
		state = GameState.ENDED;
		print ("endRace");

		NetworkManager.singleton.StopServer ();
		startServer ();
	}

	public static int getSpawnPointNum () {
		int n = 0;

		foreach (int a in _spawned.Keys)
			if (n == a)
				n++;

		return n;
	}

	public static void spawned (Unicycle unicycle) {
		_spawned [unicycle.spawnnum] = unicycle;
		print ("プレイヤーがスポーンしました: " + unicycle.name + " n: " + unicycle.spawnnum);

		//connectionsが1つ多いので注意
		if (NetworkServer.connections.Count - 1 == NetworkManager.singleton.maxConnections)
			Main.startRace ();
	}

	public static void quitted (Unicycle unicycle) {
		_spawned.Remove (unicycle.spawnnum);
		print ("プレイヤーが退出しました: " + unicycle.name);
	}

	public static Unicycle getJoinedUnicycle (int spawnnum) {
		return _spawned [spawnnum];
	}



	public static void checkPointPass (CheckPoint cp, Unicycle unicycle) {
		print ("Pass CP: " + cp + " Unicycle: " + unicycle);
	}
}
