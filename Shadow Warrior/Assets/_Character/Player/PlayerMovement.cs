using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

// TODO consider re-wire
using RPG.CameraUI; 
using RPG.Core;

// TODO extract weaponSystem 
namespace RPG.Character
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] float baseDamage = 10f;
		[SerializeField] Weapon currentWeaponConfig = null;
		[SerializeField] AnimatorOverrideController  animatorOverrideController = null; 
		[Range (.1f, 1.0f)] [SerializeField] float criticalHitChance = 0.1f;
		[SerializeField] float criticalHitMultiplier = 1.25f;
		[SerializeField] ParticleSystem criticalHitParticle = null;

		const string ATTACK_TRIGGER = "Attack";
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";

		Character character;
		Enemy enemy = null;
		Animator animator = null;
		SpecialAbilities abilities;

		CameraRaycaster cameraRaycaster = null;
		float lastHitTime = 0f;
		GameObject weaponObject;

		void Start ()
		{
			character = GetComponent<Character> ();
			abilities = GetComponent<SpecialAbilities> ();

			RegisterForMouseEvents();
			PutWeaponInHand (currentWeaponConfig); // TODO move to WeaponSystom
			SetAttackAnimation (); // TODO move to WeaponSystem

		}

		private void RegisterForMouseEvents()
		{
			cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
			cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
		}

		public void PutWeaponInHand(Weapon weaponToUse)
		{
			currentWeaponConfig = weaponToUse;
			var weaponPrefab = weaponToUse.GetWeaponPrefab ();
			GameObject dominantHand = RequestDominantHand ();
			Destroy (weaponObject); // empty hands
			weaponObject = Instantiate (weaponPrefab, dominantHand.transform);
			weaponObject.transform.localPosition = currentWeaponConfig.gripTransform.localPosition;
			weaponObject.transform.localRotation = currentWeaponConfig.gripTransform.localRotation;
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
				AttackTarget();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				abilities.AttemptSpecialAbility(0);
			}
		}

		//TODO move to Weapon System
		/* public void PutWeaponInHand(Weapon weaponToUse)
		{
			currentWeaponConfig = weaponToUse;
			var weaponPrefab = weaponToUse.GetWeaponPrefab ();
			GameObject dominantHand = RequestDominantHand ();
			Destroy(weaponObject); // empty hands
			weaponObject = Instantiate(weaponPrefab, dominantHand.transform);
			weaponObject.transform.localPosition = currentWeaponConfig.gripTransform.localPosition;
			weaponObject.transform.localRotation = currentWeaponConfig.gripTransform.localRotation;
		}*/

		private void SetAttackAnimation ()
			{
				var animator = GetComponent<Animator>();
				animator.runtimeAnimatorController = animatorOverrideController;
			animatorOverrideController[DEFAULT_ATTACK] = currentWeaponConfig.GetAttackAnimClip(); 
			}

		private GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();
			int numberOfDominantHands = dominantHands.Length;
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found on Player ");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts found on Player, Please remove one ");
			return dominantHands[0].gameObject;
		}

		// TODO use co-routines for move and attack
		private void AttackTarget ()
			{
			if (Time.time - lastHitTime > currentWeaponConfig.GetMinTimeBetweenHits())
				{
					SetAttackAnimation();
					animator.SetTrigger(ATTACK_TRIGGER);
					lastHitTime = Time.time;
				}
			}
		// TODO move to weapon system
		private float CalculateDamage()
		{
			bool isCriticalHit = UnityEngine.Random.Range(0f, 1f) <= criticalHitChance;
			float damageBeforeCritical = baseDamage + currentWeaponConfig.GetAdditionalDamage();
			if (isCriticalHit)
			{
				criticalHitParticle.Play();
				return damageBeforeCritical * criticalHitMultiplier;
			}
			else
			{
				return damageBeforeCritical;
			}
		}

			private bool IsTargetInRange(GameObject target)
			{
				float distanceToTarget = (target.transform.position - transform.position).magnitude;
			return distanceToTarget <= currentWeaponConfig.GetMaxAttackRange();
			}
		}
}