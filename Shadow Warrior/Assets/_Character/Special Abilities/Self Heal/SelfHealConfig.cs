using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Self Heal"))]
	public class SelfHealConfig : SpecialAbility 
	{
		[Header("Self Heal Speific")]
		[SerializeField] float extraHealth= 50f;

		public override ISpecialAbility AddComponent (GameObject gameObjectToattachTo)
		{
			var behaviourComponent = gameObjectToattachTo.AddComponent<SelfHealBehaviour> ();
			behaviourComponent.SetConfig(this);
			behaviour = behaviourComponent;
		}

		public float GetExtraHealth()
		{
			return extraHealth;
		}
	}
}