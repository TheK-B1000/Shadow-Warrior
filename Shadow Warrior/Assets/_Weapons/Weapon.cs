﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
[CreateAssetMenu(menuName = ("RPG/Weapon"))]
public class Weapon : ScriptableObject {

		public Transform gripTransform;

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnimation;
		[SerializeField] float minTimeBetweenHits = .5f;
		[SerializeField] float maxAttackRange = 2f;

		public float GetMinTimeBetweenHits()
		{
			//TODO consider whether we take animation time into account
			return minTimeBetweenHits;
		}

		public float GetMaxAttack()
		{
			return maxAttackRange;
		}

		public GameObject GetWeaponPrefab()
		{
			return weaponPrefab;
		}
		public AnimationClip GetAttackAnimClip()
		{
			RemoveAnimationEvents (); 
			return attackAnimation;
		}

		//So that asset packs cannot cause crashes
		void RemoveAnimationEvents ()
		{
			attackAnimation.events = new AnimationEvent[0];
		}
	}
}