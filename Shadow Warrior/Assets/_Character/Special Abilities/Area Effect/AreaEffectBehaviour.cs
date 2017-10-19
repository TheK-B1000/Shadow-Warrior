using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
using RPG.Core;
using System;

public class AreaEffectBehaviour : AbilityBehaviour 
{
		
	public override void Use(GameObject target)
	{
		PlayAbilitySound ();
		DealRadialDamage ();
		PlayParticleEffect ();
	}

	private void DealRadialDamage()
	{
		print ("Area Effect used by " + gameObject.name);
		// static sphere cast for targets
		RaycastHit[] hits = Physics.SphereCastAll(
			transform.position,
			(config as AreaEffectConfig).GetRadius(),
			Vector3.up,
			(config as AreaEffectConfig).GetRadius()
		);

		foreach (RaycastHit hit in hits)
		{
			var damegable = hit.collider.gameObject.GetComponent<HealthSystem> (); 
			bool hitPlayer = hit.collider.gameObject.GetComponent<Player> ();
			if (damegable != null && !hitPlayer) 
			{
				float damageToDeal = (config as AreaEffectConfig).GetDamageToEachTarget();
				damegable.TakeDamage (damageToDeal);
			}
		}
	}
}
