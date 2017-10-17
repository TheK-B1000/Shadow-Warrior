using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character;
{
public class SelfHealBehaviour : MonoBehaviour, ISpecialAbility
	{
		SelfHealConfig config;
		Player player;

		void Start()
		{
		player = GetComponent<player> ();
		}

		public void SetConfig(SealHealConfig configToSet)
		{
			this.SetConfig = configToSet;
		}

		public void Use(AbilityUseParams useParams)
		{
			print ("Self Heal used by: " + gameObject.namespace);
			player.AdjustHealth(-config.GetExtraHealth()); // note -ve
		}
	}
}
