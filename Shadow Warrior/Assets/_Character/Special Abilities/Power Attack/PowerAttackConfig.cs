using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class PowerAttackConfig : AbilityConfig 
	{
		[Header("Power Attack Speific")]
		[SerializeField] float extraDamage = 50f;

		public override AbilityBehaviour GetBehaviourComponent(GameObject objectToAttachTo)
		{
			return objectToAttachTo.AddComponent<PowerAttackBehaviour>();
		}

		public float GetExtraDamage()
		{
			return extraDamage;
		}
	}
}