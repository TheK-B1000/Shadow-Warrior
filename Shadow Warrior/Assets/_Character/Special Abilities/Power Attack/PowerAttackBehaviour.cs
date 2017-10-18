using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	public class PowerAttackBehaviour : AbilityBehaviour
	{
		AudioSource audioSource = null;


		// Use this for initialization
		void Start () {
			print ("Power Attack behaviour attached to " + gameObject.name);
			audioSource = GetComponent<AudioSource>();
		}

		public override void Use(AbilityUseParams useParams)
		{
			DealDamage(useParams);
			PlayParticleEffect();
			audioSource.clip = config.GetAudioClip();
			audioSource.Play();
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			float damageToDeal = useParams.baseDamage + (config as PowerAttackConfig).GetExtraDamage ();
			useParams.target.TakeDamage (damageToDeal);
		}
	}
}
