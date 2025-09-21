using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    private AudioSource audioSource;

    
    public event Action<Enemy> OnEnemyDeath;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! Health: {health}");

        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        
        OnEnemyDeath?.Invoke(this);

        Destroy(gameObject);
    }
}