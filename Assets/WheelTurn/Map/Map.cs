using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	//public const float ABYSS_HEIGHT = -1f;

	public static Vector3 DEFAULT_SPAWN = Vector3.zero;

	public string mapname;
	public string creator;
	public Vector3[] spawnPoints = new Vector3[8];

	public Vector3 getPlayerSpawnPoint (int rank) {
		return spawnPoints [rank];
	}
}
