using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gentlyRotate : MonoBehaviour {

	void Update () {
		transform.Rotate(Vector3.up * 6f * Time.deltaTime, Space.World);
	}
}
