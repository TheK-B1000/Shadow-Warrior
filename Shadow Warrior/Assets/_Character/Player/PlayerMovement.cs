﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using RPG.CameraUI;


namespace RPG.Character
{
	public class PlayerMovement : MonoBehaviour
	{
		[Range (.1f, 1.0f)] [SerializeField] float criticalHitChance = 0.1f;
		[SerializeField] float criticalHitMultiplier = 1.25f;
		[SerializeField] ParticleSystem criticalHitParticle = null;

		Character character;
		Enemy enemy = null;
		SpecialAbilities abilities;
		WeaponSystem weaponSystem;

		CameraRaycaster cameraRaycaster = null;

		void Start ()
		{
			character = GetComponent<Character> ();
			abilities = GetComponent<SpecialAbilities> ();
			weaponSystem = GetComponent<WeaponSystem> ();

			RegisterForMouseEvents();
	

		}

		private void RegisterForMouseEvents()
		{
			cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
			cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
		}
			

		void Update()
		{
				ScanForAbilityKeyDown();
		}

		private void ScanForAbilityKeyDown()
		{
			for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++) 
			{
				if (Input.GetKeyDown(keyIndex.ToString())) 
				{
					abilities.AttemptSpecialAbility(keyIndex);
				}
			}
		}
	
		void OnMouseOverPotentiallyWalkable(Vector3 destination)
		{
			if (Input.GetMouseButton (0))
			{
				character.SetDestination (destination);
			}
		}

		void OnMouseOverEnemy(Enemy enemyToSet)
		{
			this.enemy = enemyToSet;
			if (Input.GetMouseButton (0) && IsTargetInRange(enemy.gameObject))
			{
				weaponSystem.AttackTarget(enemy.gameObject);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				abilities.AttemptSpecialAbility(0);
			}
		}

			private bool IsTargetInRange(GameObject target)
			{
				float distanceToTarget = (target.transform.position - transform.position).magnitude;
				return distanceToTarget <= weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
			}
		}
}