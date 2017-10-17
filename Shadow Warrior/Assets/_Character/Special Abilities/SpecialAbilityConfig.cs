using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	public abstract class SpecialAbilityConfig : ScriptableObject {

		[Header("SpecialAbility General")]
		[SerializeField] float energyCost = 10f

		protected ISpecialAbility behaviour

		abstract public void ISpecialAbility AddComponent (GameObject gameObjectToattachTo);

		public void Use()
		{
		}
	}
}
