using UnityEngine;
using System.Collections;
using RPG.Character;
using RPG.Core;

public class Fireball : MonoBehaviour
{

    [SerializeField] float baseDamage = 500f;
    public float Force = 10;
    public float Radius = 10;
    public GameObject explotion;
    public float damageRadius = 50;
    public float damage = 500;
    public int force = 160;

    public bool affectPlayer = false; //Apply damage to Player?
    public bool addExplosionForceToPlayer = false; //only works when Player is rigidbody

    public bool addExplosionForceToEnemies = true; //only works when Enemies are rigidbodies

    RaycastHit hit;
    GameObject target;
    Character character;
    RaycastHit raycastHit;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9)
        {
            Rigidbody impact = other.GetComponent<Rigidbody>();

            if (impact)
            {
                impact.AddExplosionForce(100f * Force, transform.position, 100f * Radius);
                Debug.Log("Damaged Enemy");
            }


            Destroy(gameObject);
            //create fireball explotion after collides
            GameObject fireballexplotion = Instantiate(explotion);
            fireballexplotion.transform.position = transform.position;

            Destroy(fireballexplotion, 2f);

            //Explode();
        }
    }

   
}