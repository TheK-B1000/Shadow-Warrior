using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core; // TODO consider re-wire

namespace RPG.Character
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] float chaseRadius = 5.0f;

		[SerializeField] float attackRadius = 10.0f;
		[SerializeField] private float damagePerShot = 10f;
		[SerializeField] float firingPeriodInS = 0.5f;
		[SerializeField] float firingPeriodvariation = 0.1f;
		[SerializeField] GameObject projectileToUse;
		[SerializeField] GameObject projectileSocket;
		[SerializeField] Vector3 aimOffset = new Vector3 (0, 1f, 0);

		bool isAttacking = false;
		PlayerMovement player = null;

		void Start()
		{
			player = FindObjectOfType<PlayerMovement>();
		}

		public void TakeDamage(float amount)
		{
			// TODO remove
		}

		void Update ()
			{
			float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
			if (distanceToPlayer <= attackRadius && !isAttacking) 
			{
				isAttacking = true;
					float randomisedDelay = Random.Range(firingPeriodInS - firingPeriodvariation, firingPeriodInS + firingPeriodvariation);
				InvokeRepeating ("FireProjectile", 0f, firingPeriodInS); // TODO switch to coroutines
			}

			if (distanceToPlayer > attackRadius)
			{
				isAttacking = false;
				CancelInvoke ();
			}

			if (distanceToPlayer <= chaseRadius) 
			{
				//aiCharacterControl.SetTarget(player.transform);
			} 
			else 
			{
				//aiCharacterControl.SetTarget (transform);
			}
		}

		// TODO separate out Character into firing logic
		void FireProjectile(){
			GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
			Projectile projectileComponent = newProjectile.GetComponent<Projectile> ();
			projectileComponent.SetDamage(damagePerShot);
			projectileComponent.SetShooter (gameObject);

			Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
			float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
			newProjectile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * projectileSpeed;
		}

		
		void OnDrawGizmos()
		{
			// Draw move sphere
			Gizmos.color = new Color (0f , 0f, 240f, .5f);
			Gizmos.DrawWireSphere (transform.position, chaseRadius);

			// Draw attack sphere
			Gizmos.color = new Color (255f, 0f, 0, .5f);
			Gizmos.DrawWireSphere (transform.position, attackRadius);
		}
	}
}