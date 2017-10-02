using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {



	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 10.0f;
	[SerializeField] float chaseRadius = 5.0f;

	float currentHealthPoints = 100f;
	AICharacterControl aiCharacterControl = null;
	GameObject player = null;
	Projectile projectile;

	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints / (float)maxHealthPoints;
		}
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		aiCharacterControl = GetComponent<AICharacterControl> ();
	}

	// TODO CLEAN UP
	//void LaunchProjectile(){
	//	GameObject.projectile = 
	//		if(Layerhit = attackRadius)
	//		{
	//			fire(projectile);
	//		}
	//}

	void Update (){

		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius) 
		{
			print (gameObject.name + "attacking player");
			//TODO spawn projectile
		} 
		else
		{
			aiCharacterControl.SetTarget (transform);
		}
			

		if (distanceToPlayer <= chaseRadius) 
		{
			print (gameObject.name + "move to player");
			aiCharacterControl.SetTarget(player.transform);
		} 
		else 
		{
			aiCharacterControl.SetTarget (transform);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	}

	void OnDrawGizmos()
	{
		// Draw move sphere
		Gizmos.color = new Color (0f , 0f, 240f, .5f);
		Gizmos.DrawWireSphere (transform.position, chaseRadius);

		// Draw attack sphere
		Gizmos.color = new Color (255f, 0f, 0, .5f);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
	}
}
