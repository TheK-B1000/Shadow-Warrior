using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
	public class PowerAttackBehaviour : MonoBehaviour, ISpecialAbility
	{
		PowerAttackConfig config;

		public void SetConfig(PowerAttackConfig configToSet)
		{
			this.SetConfig = configToSet;
		}

		// Use this for initialization
		void Start () {
			print ("Power Attack behaviour attached to " + gameObject.name);
		}

		// Update is called once per frame
		void Update () {

		}
		void Use()
		{
			print ("Power Attack Used");
		}
	}
}
