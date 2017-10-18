﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
public abstract class AbilityBehaviour : MonoBehaviour {

		protected AbilityConfig config;

		const float PARTICLE_CLEAN_UP_DELAY = 20f;

		public abstract void Use(AbilityUseParams useParams);

		public void SetConfig(AbilityConfig configToSet)
		{
			config = configToSet;
		}

		protected void PlayParticleEffect()
		{
			var particlePrefab = config.GetParticlePrefab ();
			var particleObject = Instantiate (
				particlePrefab,
				transform.localPosition,
				particlePrefab.transform.rotation
			);
			particleObject.transform.parent = transform;
			particleObject.GetComponent<ParticleSystem>().Play();
			StartCoroutine(DestroyParticleWhenFinished(particleObject));

		}

		IEnumerator DestroyParticleWhenFinished (GameObject particlePrefab)
		{
			while (particlePrefab.GetComponent<ParticleSystem> ().isPlaying) 
			{
				yield return new WaitForSeconds (PARTICLE_CLEAN_UP_DELAY);
			}
			Destroy (particlePrefab);
			yield return new WaitForEndOfFrame();
		}
	}
}