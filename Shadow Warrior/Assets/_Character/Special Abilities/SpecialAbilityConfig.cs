using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public abstract class SpecialAbilityConfig : ScriptableObject {

		[Header("SpecialAbility General")]
		[SerializeField] float energyCost = 10f;

		abstract public ISpecialAbility AddComponent (GameObject gameObjectToattachTo);
	}
}
