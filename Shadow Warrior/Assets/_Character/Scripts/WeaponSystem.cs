﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RPG.Character
{
	public class WeaponSystem : MonoBehaviour
	{
		[SerializeField] float baseDamage = 10f;
		[SerializeField] WeaponConfig currentWeaponConfig = null;

		GameObject target;
		GameObject weaponObject;
		Animator animator;
		Character character;
		float lastHitTime;

		const string ATTACK_TRIGGER = "Attack";
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";


		// Use this for initialization
		void Start () 
		{
			animator = GetComponent<Animator> ();
			character = GetComponent<Character> ();

			PutWeaponInHand (currentWeaponConfig); 
			SetAttackAnimation (); 
		}
		
		// Update is called once per frame
		void Update () 
		{
			bool targetIsDead;
			bool targetIsOutOfRange;
			if (target == null)
			{
				targetIsDead = false;
				targetIsOutOfRange = false;
			}
			else
			{
				var targethealth = target.GetComponent<HealthSystem> ().healthAsPercentage;
				targetIsDead = targethealth <= Mathf.Epsilon;

				var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
				targetIsOutOfRange = distanceToTarget > currentWeaponConfig.GetMaxAttackRange ();
			}

			float characterHealth = GetComponent<HealthSystem> ().healthAsPercentage;
			bool characterIsDead = (characterHealth <= Mathf.Epsilon);

			if (characterIsDead || targetIsOutOfRange || targetIsDead) 
			{
				StopAllCoroutines ();
			}
		}
	


		public void PutWeaponInHand(WeaponConfig weaponToUse)
		{
			currentWeaponConfig = weaponToUse;
			var weaponPrefab = weaponToUse.GetWeaponPrefab ();
			GameObject dominantHand = RequestDominantHand ();
			Destroy(weaponObject); // empty hands
			weaponObject = Instantiate(weaponPrefab, dominantHand.transform);
			weaponObject.transform.localPosition = currentWeaponConfig.gripTransform.localPosition;
			weaponObject.transform.localRotation = currentWeaponConfig.gripTransform.localRotation;
		}

		public void AttackTarget(GameObject targetToAttack)
		{
			target = targetToAttack;
			StartCoroutine (AttackTargetRepeatedly ());
		}

		public void StopAttacking()
		{
			animator.StopPlayback ();
			StopAllCoroutines ();
		}

		IEnumerator AttackTargetRepeatedly()
		{
				bool attackersStillAlive = GetComponent<HealthSystem> ().healthAsPercentage >= Mathf.Epsilon;
				bool targetStillAlive = target.GetComponent<HealthSystem> ().healthAsPercentage >= Mathf.Epsilon;

				while (attackersStillAlive && targetStillAlive) 
				{
					float weaponHitPeriod = currentWeaponConfig.GetTimeBetweenAnimationCycles ();
				float timeToWait = weaponHitPeriod * character.GetAnimSpeedMultiplier();
					bool isTimeToHitAgain = Time.time - lastHitTime > timeToWait;

					if (isTimeToHitAgain)
					{
						AttackTargetOnce ();
						lastHitTime = Time.time;
				}
				yield return new WaitForSeconds (timeToWait);
			}
		}

		void AttackTargetOnce()
		{
			transform.LookAt (target.transform);
			animator.SetTrigger (ATTACK_TRIGGER);
			float damageDelay = currentWeaponConfig.GetDamageDelay ();
			SetAttackAnimation();
			StartCoroutine (DamageAfterDelay (damageDelay));
		}

		IEnumerator DamageAfterDelay(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			target.GetComponent<HealthSystem>().TakeDamage(CalculateDamage());
		}

		public WeaponConfig GetCurrentWeapon()
		{
			return currentWeaponConfig;
		}

		private void SetAttackAnimation ()
		{
			if (!character.GetOverrideController ()) {
				Debug.Break ();
				Debug.LogAssertion ("Please provide " + gameObject + " with an animator override controller.");
			} else {
				animator = GetComponent<Animator> ();
				var animatorOverrideController = character.GetOverrideController ();
				animator.runtimeAnimatorController = animatorOverrideController;
				animatorOverrideController [DEFAULT_ATTACK] = currentWeaponConfig.GetAttackAnimClip (); 
			}
		}

		private GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();
			int numberOfDominantHands = dominantHands.Length;
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found on Player ");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts found on Player, Please remove one ");
			return dominantHands[0].gameObject;
		}
			
		private void AttackTarget ()
		{
			if (Time.time - lastHitTime > currentWeaponConfig.GetTimeBetweenAnimationCycles())
			{
				SetAttackAnimation();
				animator.SetTrigger(ATTACK_TRIGGER);
				lastHitTime = Time.time;
			}
		}

		private float CalculateDamage()
		{
			return baseDamage + currentWeaponConfig.GetAdditionalDamage();
		}
	}
}