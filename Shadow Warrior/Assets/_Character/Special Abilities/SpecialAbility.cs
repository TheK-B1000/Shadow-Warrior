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
	public abstract class SpecialAbility : ScriptableObject {

		[Header("SpecialAbility General")]
		[SerializeField] float energyCost = 10f
		[SerializeField] 

		protected ISpecialAbility behaviour

		abstract public void ISpecialAbility AddComponent (GameObject gameObjectToattachTo);

		public void Use(AbilityUseParams useParams)
		{
			behaviour.Use(useParams);
		}
	}

	public float GetEnergyCost()
	{
		return energyCost;
	}

	public GameObject GetParticlePrefab()
	{
		return particlePrefab;
	}
}

	public interface ISpecialAbility
	{
		void use(AbilityUseParams useParams);
	}
}
