using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class AreaEffectConfig : AbilityConfig
	{
		[Header("Area Effect Speific")]
		[SerializeField] float radius = 5f;
		[SerializeField] float damageToEachTarget = 15f;

		public override AbilityBehaviour GetBehaviourComponent(GameObject objectToAttachTo)
		{
			return objectToAttachTo.AddComponent<AreaEffectBehaviour>();
		}

		public float GetDamageToEachTarget()
		{
			return damageToEachTarget;
		}
		public float GetRadius()
		{
			return radius;
		}
	}
}