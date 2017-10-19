using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
public class Projectile : MonoBehaviour {


		[SerializeField] float projectileSpeed; 
		[SerializeField] GameObject shooter; // So can ispected when paused

		const float Destroy_Delay = 0.01f;
		float damageCaused;

		public void SetShooter(GameObject shooter)
		{
			this.shooter = shooter;
		}
			
		public void SetDamage(float damage)
		{
			damageCaused = damage;
		}

		public float GetDefaultLaunchSpeed()
		{
			return projectileSpeed;
		}


		void OnCollisionEnter(Collision collision)
		{
			var layerCollidedWith = collision.gameObject.layer;
			if (shooter && layerCollidedWith != shooter.layer) 
			{
				//DamageDamegables (collision);
			}
		}


		//TODO Re-Implement 
		/* private void DamageDamegables (Collision collision)
		{
			Component damageableComponent = collision.gameObject.GetComponent (typeof(IDamageable));
			if (damageableComponent) 
			{
					(damageableComponent as IDamageable).TakeDamage (damageCaused);
			}
			Destroy (gameObject, Destroy_Delay);
		}*/
			
	}
}