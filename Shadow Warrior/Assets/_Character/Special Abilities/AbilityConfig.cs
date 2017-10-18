﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Character
{
	public struct AbilityUseParams
	{
		public IDamageable target;
		public float baseDamage;

		public AbilityUseParams(IDamageable target, float baseDamage)
		{
			this.target = target;
			this.baseDamage = baseDamage;
		}
	}

	public abstract class AbilityConfig : ScriptableObject 
	{

		[Header("Special Ability General")]
		[SerializeField] float energyCost = 10f;
		[SerializeField] GameObject particlePrefab = null;
		[SerializeField] AudioClip[] audioClips = null;
		
		protected AbilityBehaviour behaviour;

		public abstract AbilityBehaviour GetBehaviourComponent (GameObject objectToAttachTo);

		public void AttachAbilityTo(GameObject objectToattachTo)
		{
			AbilityBehaviour behaviourComponent = GetBehaviourComponent(objectToattachTo);
			behaviourComponent.SetConfig (this);
			behaviour = behaviourComponent;
		}

		public void Use(AbilityUseParams useParams)
		{
			behaviour.Use(useParams);
		}

		public float GetEnergyCost()
		{
			return energyCost;
		}

		public GameObject GetParticlePrefab()
		{
			return particlePrefab;
		}

		public AudioClip GetRandomAbilitySound()
		{
			return audioClips [Random.Range (0, audioClips.Length)];
		}
	}
}
