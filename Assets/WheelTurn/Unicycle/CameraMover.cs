using UnityEngine;

public class CameraMover : MonoBehaviour {
	public static CameraMover cameramover;
	public static Vector3 CAMERA_POS = new Vector3 (0f, 2f, -2.5f);
	public static Vector3 CAMERA_ANGLE = new Vector3 (15f, 0f, 0f);
	public static float maxDistance = 5f; //カメラの追跡が遅れたときに対象から離れない距離
	public static float min_t = 1f / 5f; //カメラの追跡力(0=動かない、1=瞬時に追跡)

	Transform target;
	Vector3 pos;
	Quaternion rot;

	//カメラは後からついてくる挙動になっており、カメラが一定距離以上離れないようになっている。
	//乗り物向けなカメラ。

	void Start () {
		cameramover = this;
	}

	void Update () {
		if (target != null) {
			rot = target.rotation;
			pos = target.position + rot * CAMERA_POS;

			float x = pos.x - transform.position.x;
			float y = pos.y - transform.position.y;
			float z = pos.z - transform.position.z;
			float t = Mathf.Max (min_t, 1f - maxDistance / Mathf.Sqrt (x * x + y * y + z * z));
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, pos.x, t), Mathf.Lerp (transform.position.y, pos.y, t), Mathf.Lerp (transform.position.z, pos.z, t));
			transform.eulerAngles = new Vector3 (Mathf.Repeat (transform.eulerAngles.x + CAMERA_ANGLE.x + (Mathf.Repeat (rot.eulerAngles.x - transform.eulerAngles.x - CAMERA_ANGLE.x + 180f, 360f) - 180f) / 2 + 180f, 360f) - 180f,
				Mathf.Repeat (transform.eulerAngles.y + CAMERA_ANGLE.y + (Mathf.Repeat (rot.eulerAngles.y - transform.eulerAngles.y - CAMERA_ANGLE.y + 180f, 360f) - 180f) / 2 + 180f, 360f) - 180f,
				Mathf.Repeat (transform.eulerAngles.z + CAMERA_ANGLE.z + (Mathf.Repeat (rot.eulerAngles.z - transform.eulerAngles.z - CAMERA_ANGLE.z + 180f, 360f) - 180f) / 2 + 180f, 360f) - 180f);
		}
	}

	public void setTarget (Transform t) {
		target = t;
	}
}
