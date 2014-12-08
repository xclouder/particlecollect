using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public GameObject particleSystemPrefab;
	public GameObject powerBar;

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			//get from position
			Vector3 screenPosition = Input.mousePosition;
			Vector3 fromPosition = Camera.main.ScreenToWorldPoint(screenPosition);

			//generate a particle system
			GameObject particle = GameObject.Instantiate(particleSystemPrefab, fromPosition, Quaternion.identity) as GameObject;

			//get to position
			Vector3 nguiPosition = powerBar.transform.position;
			Camera uiCamera = NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
			Vector3 viewportPos = uiCamera.WorldToViewportPoint(nguiPosition);
			Vector3 toPosition = Camera.main.ViewportToWorldPoint(viewportPos);

			ParticleCollectController particleCollector = particle.GetComponent<ParticleCollectController>();
			particleCollector.ShowParticleAndCollect(fromPosition, toPosition, () => {

				Debug.Log("finish");

			});
		}
	}
}
