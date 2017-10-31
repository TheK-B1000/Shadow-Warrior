using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

public class Damage : MonoBehaviour {

    [SerializeField] float baseDamage = 500f;

   void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<HealthSystem>().TakeDamage(-500);

        if (other.gameObject.layer != 9)
        {
            Rigidbody impact = other.GetComponent<Rigidbody>();

            if (impact)
            {
                Debug.Log("Killed Enemy");
            }
        }
    }
}
