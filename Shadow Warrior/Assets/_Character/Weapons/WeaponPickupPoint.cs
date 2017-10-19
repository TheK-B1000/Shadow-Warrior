using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.Character
{
	[ExecuteInEditMode]
public class WeaponPickupPoint : MonoBehaviour 
	{
		[SerializeField] Weapon weaponConfig;
		[SerializeField] AudioClip pickupSFX;

		AudioSource audioSource;

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();
		}

		void DestroyChildren()
		{
			foreach (Transform child in transform) 
			{
				DestroyImmediate (child.gameObject);
			}
		}

		// Update is called once per frame
		void Update () 
		{
			if (!Application.isPlaying)
			{
				DestroyChildren ();
				InstantiateWeapon ();
			}
		}

		void InstantiateWeapon()
		{
			var weapon = weaponConfig.GetWeaponPrefab ();
			weapon.transform.position = Vector3.zero;
			Instantiate(weapon, gameObject.transform);
		}

		void OnTriggerEnter()
		{
			FindObjectOfType<PlayerMovement>().PutWeaponInHand(weaponConfig);
			audioSource.PlayOneShot (pickupSFX);
		}
	}
}
