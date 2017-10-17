using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character;
{
public class SelfHealBehaviour : MonoBehaviour, ISpecialAbility
	{
		SelfHealConfig config = null;
		Player player = null;
		AudioSource audioSource = null;

		void Start()
		{
			player = GetComponent<player> ();
		audioSource = GetComponent<AudioSource> ();
		}

		public void SetConfig(SealHealConfig configToSet)
		{
			this.SetConfig = configToSet;
		}

		public void Use(AbilityUseParams useParams)
		{
			player.Heal(config.GetExtraHealth());
			audioSource.clip = config.GetAudioClip ();
			audioSource.Play ();
		}
	}
}
