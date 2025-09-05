using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArm : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        if (other.tag == "Player")
        {
            
          // other.GetComponent<Human>().TakeDamage(damageAmount);
        }
    }
}
