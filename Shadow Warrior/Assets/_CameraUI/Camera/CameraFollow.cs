﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
public class CameraFollow : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;


	void Start (){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void LateUpdate()
		{
			transform.position = player.transform.position + offset;
		}
	
	}
}