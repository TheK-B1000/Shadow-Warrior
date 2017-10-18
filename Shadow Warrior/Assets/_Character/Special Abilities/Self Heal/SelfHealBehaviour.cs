using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	public class SelfHealBehaviour :AbilityBehaviour
		{
			Player player = null;
			AudioSource audioSource = null;

			void Start()
			{
				player = GetComponent<Player> ();
				audioSource = GetComponent<AudioSource> ();
			}
			

		public override void Use(AbilityUseParams useParams)
			{
			player.Heal ((config as SelfHealConfig).GetExtraHealth ());
			audioSource.clip = (config as SelfHealConfig).GetAudioClip ();
				audioSource.Play ();
				PlayParticleEffect ();
			}
	}
}