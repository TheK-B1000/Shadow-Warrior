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

   /* public void Explode()
    {
        print("success");
        //*** Applying damage to Player ***\\
        GameObject Character = GameObject.FindGameObjectWithTag("Enemy");
        HealthSystem healthSystem = Character.GetComponent<HealthSystem>();
        Rigidbody myrigidbody = GetComponent<Rigidbody>();

        if (Physics.Linecast(this.transform.position, Character.transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (Vector3.Distance(Character.transform.position, this.transform.position) < damageRadius)
                {
                    float proximity = (this.transform.position - Character.transform.position).magnitude;
                    float effect = 1 - (proximity / damageRadius);
                    hit.collider.SendMessageUpwards("ApplyDamage", damage * effect, SendMessageOptions.DontRequireReceiver);
                    if (hit.collider.myrigidbody && addExplosionForceToPlayer)
                    {
                        hit.collider.myrigidbody.AddExplosionForce(force, this.transform.position, damageRadius, 3.0f);

                        //*** Applying damage to enemies ***\\
                        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Shootable"); //FINDS ENEMIES BASED ON TAG
                        foreach (GameObject Enemy in Enemies)
                        {
                            HealthSystem healthSystem = Character.GetComponent<HealthSystem>();
                            if (!healthSystem)
                            {
                                continue;
                            }

                            if (Physics.Linecast(this.transform.position, Enemy.transform.position, out hit))
                            {
                                if (hit.collider.gameObject.CompareTag("Enemy"))
                                {
                                    if (Vector3.Distance(Enemy.transform.position, this.transform.position) < damageRadius)
                                    {
                                        float proximity = (this.transform.position - Enemy.transform.position).magnitude;
                                        float effect = 1 - (proximity / damageRadius);
                                        hit.collider.SendMessageUpwards("ApplyDamage", damage * effect, SendMessageOptions.DontRequireReceiver);
                                        if (hit.collider.myrigidbody && addExplosionForceToEnemies)
                                        {
                                            hit.collider.myrigidbody.AddExplosionForce(force, this.transform.position, damageRadius, 3.0f);
                                        }
                                    }
                                }
                            }
                        }

                        //*** Applying force to rigidbodies ***\\
                        Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
                        foreach (Collider colliderHit in colliders)
                        {
                            if (!colliderHit)
                            {
                                continue;
                            }
                            if (Physics.Linecast(this.transform.position, colliderHit.transform.position, out hit))
                            {
                                if (hit.rigidbody)
                                {
                                    hit.rigidbody.AddExplosionForce(force, this.transform.position, damageRadius, 3.0f);
                                }
                            }
                        }


                        float CharacterHealth()
                        {
                            HealthSystem healthSystem = Character.GetComponent<HealthSystem>();
                        }
                    }*/
                }