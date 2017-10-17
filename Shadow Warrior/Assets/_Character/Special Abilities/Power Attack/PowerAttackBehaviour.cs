using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	public class PowerAttackBehaviour : MonoBehaviour, ISpecialAbility
	{
		PowerAttackConfig config;

		public void SetConfig(PowerAttackConfig configToSet)
		{
			this.SetConfig = configToSet;
		}

		// Use this for initialization
		void Start () {
			print ("Power Attack behaviour attached to " + gameObject.name);
		}

		public void Use(AbilityUseParams useParams)
		{
			DealDamage (useParams);
			PlayParticleEffect ();
		}

		private void PlayParticleEffect ()
		{
			var prefab = Instantiate (config.GetParticlePrefab(), transform.position, Quaternion.identity);
			//TODO decide if particle system attaches to player
			myParticleSystem = prefab.GetComponent<ParticleSystem>();
			myParticleSystem.Play ();
			Destroy (prefab, myParticleSystem.main.duration);

		}

		void Use(AbilityUseParams useParams)
		{
			float damageToDeal = useParams.baseDamage + config.GetExtraDamage ();
			useParams.target.TakeDamage (damageToDeal);
		}
	}
}
