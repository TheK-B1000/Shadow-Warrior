﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character
{
	public class SpecialAbilities : MonoBehaviour
		{
			[SerializeField] AbilityConfig[] abilities;
			[SerializeField] Image energyBar;
			[SerializeField] float maxEnergyPoints = 100f;
			[SerializeField] float regenPointsPerSecond = 1f;
			// TODO add outOfEnergy;

		float currentEnergyPoints;
		AudioSource audioSource;

		float energyAsPercent { get { return currentEnergyPoints / maxEnergyPoints; } }

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();
			currentEnergyPoints = maxEnergyPoints;
			AttachInitialAbilities ();
			UpdateEnergyBar ();
		}

		void Update()
		{
			if (currentEnergyPoints < maxEnergyPoints)
			{
				AddEnergyPoints ();
				UpdateEnergyBar ();
			}
		}

		void AttachInitialAbilities()
		{
			for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++)
			{
				abilities[abilityIndex].AttachAbilityTo(gameObject);
			}
		}

		public void AttemptSpecialAbility(int abilityIndex)
		{
			var energyComponent = GetComponent<SpecialAbilities>();
			var energyCost = abilities[abilityIndex].GetEnergyCost();

			if (energyCost <= currentEnergyPoints) 
			{
				ConsumeEnergy (energyCost);
				print("Using special ability" + abilityIndex); //TODO make work
			} 
			else 
			{
				// TODO play out of energy sound
			}
		}

		public int GetNumberOfAbilities()
		{
			return abilities.Length;
		}

		private void AddEnergyPoints()
		{
			var pointsToAdd = regenPointsPerSecond * Time.deltaTime;
			currentEnergyPoints = Mathf.Clamp (currentEnergyPoints + pointsToAdd, 0, maxEnergyPoints);
		}

		public void ConsumeEnergy(float amount)
		{
			float newEnergyPoints = currentEnergyPoints - amount;
			currentEnergyPoints = Mathf.Clamp (newEnergyPoints, 0, maxEnergyPoints);
			UpdateEnergyBar ();
		}

		private void UpdateEnergyBar()
		{
			energyBar.fillAmount = energyAsPercent;
		}
	}
}