﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character
{
	[RequireComponent(typeof(WeaponSystem))]
	public class EnemyAI : MonoBehaviour
	{
		
		[SerializeField] float chaseRadius = 5.0f;
		[SerializeField] WaypointContainer patrolPath;
		[SerializeField] float waypointTolerance = 2.0f;

		PlayerMovement player = null;
		Character character;
		int nextWaypointIndex;
		float currentWeaponRange;
		float distanceToPlayer;

		enum State { idle, patrolling, attacking, chasing }
		State state = State.idle;

		void Start()
		{
			character = GetComponent<Character> ();
			player = FindObjectOfType<PlayerMovement>();
		}

		void Update ()
			{
			float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
			WeaponSystem weaponSystem = GetComponent<WeaponSystem> ();
			currentWeaponRange = weaponSystem.GetCurrentWeapon ().GetMaxAttackRange ();
			if (distanceToPlayer > chaseRadius && state != State.patrolling)
			{
				StopAllCoroutines ();
				StartCoroutine(Patrol());
			}
			if (distanceToPlayer <= chaseRadius && state != State.chasing) 
			{
				StopAllCoroutines ();
				StartCoroutine(ChasePlayer());
							}
			if (distanceToPlayer <= currentWeaponRange && state != State.attacking)
			{
				StopAllCoroutines();
				state = State.attacking;
			}
		}

		IEnumerator Patrol()
		{
			state = State.patrolling;

			while (true) 
			{
				Vector3 nextWaypointPos = patrolPath.transform.GetChild (nextWaypointIndex).position;
				character.SetDestination (nextWaypointPos);
				CycleWaypointWhenClose(nextWaypointPos);
				yield return new WaitForSeconds (0.5f); // TODO paramterise
			}
		}

		private void CycleWaypointWhenClose(Vector3 nextWaypointPos)
		{
			if (Vector3.Distance (transform.position, nextWaypointPos) <= waypointTolerance)
			{
				
			}
			nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
		}

		IEnumerator ChasePlayer()
		{
			state = State.chasing;
			while (distanceToPlayer >= currentWeaponRange)
			{
				character.SetDestination (player.transform.position);
				yield return new WaitForEndOfFrame ();
			}
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