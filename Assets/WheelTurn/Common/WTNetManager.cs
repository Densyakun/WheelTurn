using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class WTNetManager : NetworkManager {

	public override void OnClientDisconnect (NetworkConnection conn) {
		base.OnClientDisconnect (conn);
		Main.title ();
	}

	public override void OnServerDisconnect (NetworkConnection conn) {
		//TODO エラー回避
		//base.OnServerDisconnect (conn);

		foreach (NetworkInstanceId netId in conn.clientOwnedObjects) {
			print (netId.Value);
			bool b = false;

			foreach (Unicycle v in Main._spawned.Values) {
				if (netId.Value == v.netId.Value) {
					b = true;
					Main.quitted (v);
					break;
				}
			}
		}
		//TODO プレイヤーが抜けるとスタート地点が空くようにする。
		if (Main.state != Main.GameState.WAITING)
			maxConnections -= 1;
	}

	public override void OnStartServer () {
		NetworkManager.singleton.maxConnections = Main.MAX_PLAYERS;
		base.OnStartServer ();

		Main.waitGame ();
	}

	public override void OnStopServer () {
		base.OnStopServer ();

		//サーバーを停止すると、サーバーにあるクライアントのオブジェクトが消えないバグの対策
		foreach (Unicycle u in FindObjectsOfType<Unicycle> ()) {
			Destroy (u.gameObject);
		}

		Main.title ();
	}
}
