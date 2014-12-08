using UnityEngine;
using System.Collections;

public class ParticleCollectController : MonoBehaviour {

	public float collectDelay = 1f;
	public float collectDuration = 0.5f;

	public void ShowParticleAndCollect(Vector3 from, Vector3 to, System.Action callback)
	{
		if (particleSystem == null)
		{
			Debug.LogError("explodeParticlePrefab not a particleSys");
			return;
		}

		StartCoroutine(StartShowParticleAndCollect(from, to, callback));
	}


	private IEnumerator StartShowParticleAndCollect(Vector3 from, Vector3 to, System.Action callback)
	{

		transform.position = from;

		float moveTime = 0f;
		particleSystem.Play();

		yield return new WaitForSeconds(collectDelay);

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
		while (moveTime < collectDuration)
		{
			int count = particleSystem.GetParticles(particles);

			for (int i=0; i<count; i++) {
				Vector3 pos = Vector3.Lerp(particles[i].position, to, moveTime / collectDuration);
				particles[i].position = pos;
			}
			particleSystem.SetParticles(particles, count);

			moveTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();

		}

		int c = particleSystem.GetParticles(particles);
		
		for (int i=0; i<c; i++) {
			particles[i].position = to;
		}
		particleSystem.SetParticles(particles, c);

		//once loop is finished, clear particles
		particleSystem.Clear();

		if (callback != null)
		{
			callback();
		}

		Destroy(gameObject);
	}
}
