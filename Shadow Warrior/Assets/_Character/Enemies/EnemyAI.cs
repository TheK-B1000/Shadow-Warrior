using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character
{
	[RequireComponent(typeof(WeaponSystem))]
	public class EnemyAI : MonoBehaviour
	{
		
		[SerializeField] float chaseRadius = 5.0f;

		bool isAttacking = false; // TODO more rich state
		PlayerMovement player = null;
		float currentWeaponRange;

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
			WeaponSystem weaponSystem = GetComponent<WeaponSystem> ();
			currentWeaponRange = weaponSystem.GetCurrentWeapon ().GetMaxAttackRange ();
		}
		
		void OnDrawGizmos()
		{
			// Draw move sphere
			Gizmos.color = new Color (0f , 0f, 240f, .5f);
			Gizmos.DrawWireSphere (transform.position, chaseRadius);

			// Draw attack sphere
			Gizmos.color = new Color (255f, 0f, 0, .5f);
			Gizmos.DrawWireSphere (transform.position, currentWeaponRange);
		}
	}
}