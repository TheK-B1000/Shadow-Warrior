using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
using RPG.Core;
using System;

public class AreaEffectBehaviour : AbilityBehaviour {

	ParticleSystem myParticleSystem;
	AudioSource audioSource = null;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
		
	public override void Use(AbilityUseParams useParams)
	{
		DealRadialDamage (useParams);
		PlayParticleEffect ();
		audioSource.clip = config.GetAudioClip ();
		audioSource.Play();
	}

	private void DealRadialDamage(AbilityUseParams useParams)
	{
		print ("Area Effect used by " + gameObject.name);
		// static sphere cast for targets
		RaycastHit[] hits = Physics.SphereCastAll(
			transform.position,
			(config as AreaEffectConfig).GetRadius(),
			Vector3.up,
			(config as AreaEffectConfig).GetRadius()
		);

		foreach (RaycastHit hit in hits)
		{
			var damegable = hit.collider.gameObject.GetComponent<IDamageable> (); 
			bool hitPlayer = hit.collider.gameObject.GetComponent<Player> ();
			if (damegable != null && !hitPlayer) 
			{
				float damageToDeal = useParams.baseDamage + (config as AreaEffectConfig).GetDamageToEachTarget();
				damegable.TakeDamage (damageToDeal);
			}
		}
	}
}
