using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {


	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] int enemyLayer = 9;
	[SerializeField] float damagePerHit = 10f;
	[SerializeField] float minTimeBetweenHits = .5f;
	[SerializeField] float maxAttackRange = 2f;

	[SerializeField] Weapon weaponInUse;
	[SerializeField] GameObject weaponSocketRight;
	[SerializeField] GameObject weaponSocketLeft;


	GameObject currentTarget;
	float currentHealthPoints;
	CameraRaycaster cameraRaycaster;
	float lastHitTime = 0f;

	public float healthAsPercentage{ get { return currentHealthPoints / (maxHealthPoints); } }

	void Start () {
		RegisterForMouseClick ();
		currentHealthPoints = maxHealthPoints;
		PutWeaponInHand ();

	}

	private void PutWeaponInHand()
	{
		var weaponPrefab = weaponInUse.GetWeaponPrefab();
		var weapon = Instantiate(weaponPrefab, weaponSocketRight.transform, weaponSocketLeft.transform);

		// TODO move to correct place and child to hand
		weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
		weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;

	}

	private void RegisterForMouseClick()
	{
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
	}

	void OnMouseClicked(RaycastHit raycastHit, int layerHit) 
	{
		if (layerHit == enemyLayer)
		{
			DealDamage (enemyLayer);
			var enemy = raycastHit.collider.gameObject;

			// Check enemy is in range
			if ((enemy.transform.position - transform.position).magnitude > maxAttackRange) 
			{
				return;
			}

			currentTarget = enemy;

			var enemyComponent = enemy.GetComponent<Enemy> ();
			if (Time.time - lastHitTime > minTimeBetweenHits)
			{
				enemyComponent.TakeDamage (damagePerHit);
				lastHitTime = Time.time;
			}
		}
	}

	public void DealDamage (float damage) 
	{
		print ("Damaged enemy");
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	
	}

	void OnTriggerEnter(Collider collider){

		Component damageableComponent = collider.gameObject.GetComponent (typeof(IDamageable));
		if (damageableComponent) 
		{
			(damageableComponent as IDamageable).TakeDamage (damagePerHit);
		}
	}
}
