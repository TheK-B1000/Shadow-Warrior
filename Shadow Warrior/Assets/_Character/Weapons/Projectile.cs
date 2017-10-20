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
	}
}