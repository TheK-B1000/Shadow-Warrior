using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class PowerAttackBehaviour : SpecialAbilityConfig 
	{
		[Header("Power Attack Speific")]

		public override ISpecialAbility AddComponent (GameObject gameObjectToattachTo)
		{
			var behaviourComponent = gameObjectToattachTo.AddComponent<PowerAttackBehaviour> ();
			behaviourComponent.SetConfig(this);
			return behaviourComponent;
		}
}
