using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class AreaEffectConfig : SpecialAbility
	{
		[Header("Area Effect Speific")]
		[SerializeField] float radius = 5f;
		[SerializeField] float damageToEachTarget = 15f;

		public override ISpecialAbility AddComponent (GameObject gameObjectToattachTo)
		{
			var behaviourComponent = gameObjectToattachTo.AddComponent<AreaEffectBehaviour> ();
			behaviourComponent.SetConfig(this);
			behaviour = behaviourComponent;
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