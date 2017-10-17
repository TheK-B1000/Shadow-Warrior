using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character
{

	[RequireComponent(typeof(Image))]
	public class PlayerHealthBar : MonoBehaviour
	{

		Image healthOrb;;
    public Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBarImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    	{
			healthOrb.fillAmount = player.healthAsPercentage;
    	}
	}
}
