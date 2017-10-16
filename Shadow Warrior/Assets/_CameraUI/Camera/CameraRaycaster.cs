using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using RPG.Character; // So we can detect by type

namespace RPG.CameraUI
{
public class CameraRaycaster : MonoBehaviour // TODO Rename Cursor
{
	[SerializeField] Vector2 cursorHotspot = new Vector2 (0, 0);
	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D enemyCursor = null;

	const int POTENTIALLY_WALKABLE_LAYER = 8;
    float maxRaycastDepth = 100f; // Hard coded value

		// TODO remove once working
	int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

	public delegate void OnMouseOverTerrain(Vector3 destination); 
	public event OnMouseOverTerrain notifyMouseOverTerrainObservers;

	public delegate void OnMouseOverEnemy(Enemy enemy); 
	public event OnMouseOverEnemy notifyMouseOverEnemyObservers;

	// TODO remove old delegates below
	// Setup delegates for broadcasting layer changes to other classes
    public delegate void OnCursorLayerChange(int newLayer); // declare new delegate type
    public event OnCursorLayerChange notifyLayerChangeObservers; // instantiate an observer set

	public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit); // declare new delegate type
	public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set

	public delegate void OnRightClick(RaycastHit raycastHit, int layerHit); // declare new delegate type
	public event OnClickPriorityLayer notifyRightClickObservers; // instantiate an observer set

    void Update()
		{
			// Check if pointer is over an interactable UI element
			if (EventSystem.current.IsPointerOverGameObject ()) {
				// Impliment UI Interaction
			} else {
				PerformRaycasts ();
			}
		}

		void PerformRaycasts()
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//if (RaycastForEnemy (ray)) {return;}
			if (RaycastForPotentiallyWalkable (ray)) {return;}
			FarTooComplex ();	// TODO remove	
		}

		private bool RaycastForPotentiallyWalkable(Ray Ray)
			{
			RaycastHit hitInfo;
			LayerMask potentiallyWalkable = 1 << POTENTIALLY_WALKABLE_LAYER;
			bool potentiallyWalkableHit = Physics.Raycast (Ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer);
				if (potentiallyWalkableHit)
				{
					Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
					onMouseOverPotentiallyWalkable(hitInfo.point);
					return true;
				}
			return false;
		}

		private bool RaycastForEnemy(Ray Ray)
			{
			RaycastHit hitInfo;
			Physics.Raycast (Ray, out hitInfo, maxRaycastDepth);
			var gameObject = hitInfo.collider.gameObject;
			var enemyHit = gameObject.GetComponent<Enemy> ();
			if (enemyHit)
			{
				Cursor.SetCursor (enemyCursor, cursorHotspot, CursorMode.Auto);
				onMouseOverEnemy (enemyHit);
				return true;
			}
			return false;
		}
			

	
	}
}