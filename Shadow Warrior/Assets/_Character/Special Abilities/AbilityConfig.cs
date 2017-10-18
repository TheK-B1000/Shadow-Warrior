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
		[SerializeField] AudioClip audioClip = null;
		
		protected AbilityBehaviour behaviour;

		abstract public void AttachComponentTo(GameObject gameObjectToattachTo);

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

		public AudioClip GetAudioClip()
		{
			return audioClip;
		}
	}
}
