using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

// TODO consider re-wire
using RPG.CameraUI; 
using RPG.Core; 
using RPG.Weapons;

namespace RPG.Character
{
public class Player : MonoBehaviour, IDamageable {

		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float damagePerHit = 10f;
		[SerializeField] Weapon weaponInUse = null;
		[SerializeField] AnimatorOverrideController  animatorOverrideController = null; 

		// Temporarily serialized for dubbing
		[SerializeField] SpecialAbilityConfig ability1;
	
		Animator animator;
		float currentHealthPoints;
		CameraRaycaster cameraRaycaster;
		float lastHitTime = 0f;

		public float healthAsPercentage{ get { return currentHealthPoints / (maxHealthPoints); } }

		void Start ()
		{
			RegisterForMouseClick ();
			SetCurrentMaxHealth ();
			PutWeaponInHand ();
			SetupRuntimeAnimator ();
			ability1.AddComponent (gameObject);
		}

		public void TakeDamage(float damage)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

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

		void OnMouseOverEnemy(Enemy enemy)
		{
			if (Input.GetMouseButton (0) && IsTargetInRange(enemy.gameObject))
			{
				AttackTarget (enemy);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				AttemptSpecialAbility1 (enemy);
			}
		}

		private void AttemptSpecialAbility1 (Enemy enemy)
		{
			var energyComponent = GetComponent<Energy> ();

			if (energyComponent.IsEnergyAvailable (10f)); // TODO read from S0
			{
				energyComponent.ConsumeEnergy (10f);
				// TODO Use the ability
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

		private void AttackTarget (Enemy enemy)
			{
			if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
				{
					animator.SetTrigger("Attack"); // TODO make const
					enemy.TakeDamage (damagePerHit);
					lastHitTime = Time.time;
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