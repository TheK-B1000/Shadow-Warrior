using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {


	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D targetCursor = null;
	[SerializeField] Texture2D errorCursor = null;
	[SerializeField] Vector2 cursorHotspot = new Vector2 (96, 96);

	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster> ();
	}

	// Update is called once per frame
	void Update () {
		switch (cameraRaycaster.layerHit)
		{
		case Layer.Walkable:
		Cursor.SetCursor (walkCursor, cursorHotspot, CursorMode.Auto);
		break;

		case Layer.Enemy:
		Cursor.SetCursor (targetCursor, cursorHotspot, CursorMode.Auto);
		break;

		default:
		Cursor.SetCursor (errorCursor, cursorHotspot, CursorMode.Auto);
		return;
		}
	}

}
