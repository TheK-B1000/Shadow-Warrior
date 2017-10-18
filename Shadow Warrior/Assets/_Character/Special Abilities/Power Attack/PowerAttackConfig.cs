﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class PowerAttackConfig : AbilityConfig 
	{
		[Header("Power Attack Speific")]
		[SerializeField] float extraDamage = 10f;

		public override void AttachComponentTo(GameObject gameObjectToattachTo)
		{
			var behaviourComponent = gameObjectToattachTo.AddComponent<PowerAttackBehaviour>();
			behaviourComponent.SetConfig(this);
			behaviour = behaviourComponent;
		}

		public float GetExtraDamage()
		{
			return extraDamage;
		}
	}
}