using System;
using UnityEngine;

public class Knife : MonoBehaviour
{
   private int Damage = 25;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Enemy"))
      {
         Enemy enemy = GetComponent<Enemy>();
         {
            if (enemy != null)
            {
               enemy.TakeDamage(Damage);
               Debug.Log("took Damage");
            }
         }
      }
   }
}
