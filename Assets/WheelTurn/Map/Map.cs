using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	//TODO public const float ABYSS_HEIGHT = -1f;

	public static Vector3 DEFAULT_SPAWN = Vector3.zero;
	public static Map playingmap;
	public static Vector3 ROTATE_FOR_BLEND = new Vector3 (90f, 0f, 0f);

	public string mapname;
	public string creator;
	public Transform[] spawnPoints = new Transform[8];
	private Vector3[] spawnPointsP;
	private Vector3[] spawnPointsE;
	public CheckPoint[] checkPoints = new CheckPoint[3];
	public bool isBlendFile = false;

	void Awake () {
		spawnPointsP = new Vector3[spawnPoints.Length];
		spawnPointsE = new Vector3[spawnPoints.Length];
		for (int a = 0; a < spawnPoints.Length; a++) {
			spawnPointsP [a] = spawnPoints [a].position;
			spawnPointsE [a] = spawnPoints [a].eulerAngles;
			if (isBlendFile)
				spawnPointsE [a] += ROTATE_FOR_BLEND;
		}
	}

	void Start () {
		playingmap = this;
	}

	public Vector3 getPlayerSpawnPointPosition (int n) {
		return spawnPointsP [n];
	}

	public Vector3 getPlayerSpawnPointEuler (int n) {
		return spawnPointsE [n];
	}

	public static Map openMap (Map map) {
		closeMap ();

		return Instantiate (map);
	}

	public static void closeMap () {
		if (playingmap != null) {
			Destroy (playingmap.gameObject);
			playingmap = null;
		}
	}
}
