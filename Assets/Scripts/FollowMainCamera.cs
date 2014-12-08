using UnityEngine;
using System.Collections;

public class FollowMainCamera : MonoBehaviour {

	private Transform tr;

	void Start()
	{
		tr = transform;
	}

	// Update is called once per frame
	void LateUpdate () {

		tr.position = Camera.main.transform.position;

	}
}
