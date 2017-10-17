﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
using RPG.Core;

public class AreaEffectBehaviour : MonoBehaviour, ISpecialAbility {

	AreaEffectConfig config;
	ParticleSystem myParticleSystem;

	public void SetConfig(AreaEffectConfig configToSet)
	{
		this.config = configToSet;
	}

	// Use this for initialization
	void Start ()
	{
		print ("Area Effect behaviour attached to " + gameObject.name);
	}

	public void Use(AbilityUseParams useParams)
	{
		DealRadialDamage (useParams);
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

	private void DealRadialDamage(AbilityUseParams useParams)
	{
		print ("Area Effect used by " + gameObject.name);
		// static sphere cast for targets
		RaycastHit[] hits = Physics.SphereCastAll(
			transform.position,
			config.GetRadius(),
			Vector3.useParams,
			config.GetRadius()
		);

		foreach (RaycastHit hit in hits)
		{
			var damegable = hit.collider.gameObject.GetComponent<IDamageable> (); 
			if (damegable != null) 
			{
				float damageToDeal = useParams.baseDamage + config.GetDamageToEachTarget ();
				damegable.TakeDamage ();
			}
		}
	}
}
