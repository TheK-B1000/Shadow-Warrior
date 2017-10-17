﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

// TODO consider re-wire
using RPG.CameraUI; 
using RPG.Core; 
using RPG.Weapons;

namespace RPG.Character
{
public class Player : MonoBehaviour, IDamageable {

		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float baseDamage = 10f;
		[SerializeField] Weapon weaponInUse = null;
		[SerializeField] AnimatorOverrideController  animatorOverrideController = null; 
		[SerializeField] AudioClip[] damageSounds;
		[SerializeField] AudioClip[] deathSounds;
		[Range (.1f, 1.0f)] [SerializeField] float criticalHitChance = 0.1f;
		[SerializeField] float criticalHitMultipler = 1.25f;
		[SerializeField] ParticleSystem myParticleSystem = null;

		// Temporarily serialized for dubbing
		[SerializeField] SpecialAbility[] abilities;
	
		const string DEATH_TRIGGER = "Death";
		const string ATTACK_TRIGGER = "Attack";


		Enemy enemy = null;
		Animator animator = null;
		float currentHealthPoints = 0f;
		CameraRaycaster cameraRaycaster = null;
		float lastHitTime = 0f;

		public float healthAsPercentage{ get { return currentHealthPoints / (maxHealthPoints); } }

		void Start ()
		{
			AudioSource = GetComponent<AudioSource> ()

			RegisterForMouseClick ();
			SetCurrentMaxHealth ();
			PutWeaponInHand ();
			SetupRuntimeAnimator ();
			AttachInitialAbilities ();

		}

		private void AttachInitialAbilities()
		{
			for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++);
			{
				abilities[abilityIndex].AttachComponentTo(gameObject);
			}
	}

		void Update()
		{
			if (healthAsPercentage > Mathf.Epsilon) 
			{
				ScanForAbilityKeyDown ();
			}
		}

		private void ScanForAbilityKeyDown()
		{
			for (int keyIndex = 1; keyIndex < abilities.Length; keyIndex++) 
			{
				if (Input.GetKeyDown(keyIndex.ToString()))
				}
			}
		}

		public void AdjustHealth(float changePoints)
		{
			currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, max HealthPoints);
			ReduceHealth (damage);
			if (playerDies)  
			{
				StartCoroutine (KillPlayer ());
			}
		}
		public void Heal(float points)
		{
			currentHealthPoints = Mathf.Clamp(currentHealthPoints + damage, 0f, max HealthPoints);
		}

		IEnumerator KillPlayer()
		{
			animator.SetTrigger (DEATH_TRIGGER);

			yield return new WaitForSecondsRealtime (2f); // todo use auddio clip length (optional)

			SceneManager.LoadSceneMode(0);
			// play death sound (optional)
		}

		private void ReduceHealth (float damage)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			//play sound
		}

		private void SetCurrentMaxHealth ()
			{
				currentHealthPoints = maxHealthPoints;
			}

		private void SetupRuntimeAnimator ()
			{
				var animator = GetComponent<Animator>();
				animator.runtimeAnimatorController = animatorOverrideController;
				animatorOverrideController["DEFAULT ATTACK"] = weaponInUse.GetAttackAnimClip(); // remove parameter
			}

		private void PutWeaponInHand()
		{
			var weaponPrefab = weaponInUse.GetWeaponPrefab();
			GameObject dominantHand = RequestDominantHand ();
			var weapon = Instantiate (weaponPrefab, dominantHand.transform);
			weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
			weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;

		}

		private GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();
			int numberOfDominantHands = dominantHands.Length;
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found on Player ");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts found on Player, Please remove one ");
			return dominantHands[0].gameObject;
		}

		private void RegisterForMouseClick()
		{
			cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
		}

		void OnMouseOverEnemy(Enemy enemyToSet)
		{
			this.enemy = enemy;
			if (Input.GetMouseButton (0) && IsTargetInRange(enemy.gameObject))
			{
				AttackTarget (enemy);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				AttemptSpecialAbility1 (enemy);
			}
		}

		private void AttemptSpecialAbility1 (int abilityIndex)
		{
			var energyComponent = GetComponent<Energy> ();
			var energyCost = abilities [abilityIndex].Use (abilityParams);

			if (energyComponent.IsEnergyAvailable (energyCost))
			{
				energyComponent.ConsumeEnergy (energyCost);
				var abilityParams = new AbilityUseParams(enemy, baseDamage)
				abilities[abilityIndex].Use (abilityParams);
			}
		}

		void OnMouseClicked(RaycastHit raycastHit, int layerHit) 
		{
			if (layerHit == enemyLayer)
			{
				var enemy = raycastHit.collider.gameObject;
					if (IsTargetInRange (enemy))
					{
						AttackTarget (enemy);
					}
			}
		}

		private void AttackTarget ()
			{
			if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
				{
					animator.SetTrigger(ATTACK_TRIGGER); // TODO make const
					enemy.TakeDamage(CalculateDamage());
					lastHitTime = Time.time;
				}
			}

		private float CalculateDamage()
		{
			bool is CriticalHit = UnityEngine.Random.Range(0f, 1f) <= criticalHitChance;
			float damageBeforeCritical = baseDamage + weaponInUse.GetAdditionalDamage());
			if (isCriticalHit)
			{
			criticalHitParticle.Play();
				return damageBeforeCritical * criticalHitMultipler;
			}
			else
			{
				return damageBeforeCritical;
			}
		}

			private bool IsTargetInRange(GameObject target)
			{
				float distanceToTarget = (target.transform.position - transform.position).magnitude;
				return distanceToTarget <= weaponInUse.GetMaxAttackRange();
			}

		public void DealDamage (float damage) 
		{
			print ("Damaged enemy");
		}
	}
}