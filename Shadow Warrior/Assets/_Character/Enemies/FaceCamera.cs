﻿using UnityEngine;

namespace RPG.Character
{
// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class FaceCamera : MonoBehaviour 
	{
	    Camera cameraToLookAt;

	    // Use this for initialization 
	    void Start()
	    {
	        cameraToLookAt = Camera.main;
	    }

	    // Update is called once per frame 
	    void LateUpdate()
	    {
	        transform.LookAt(cameraToLookAt.transform);   
	    }
	}
}