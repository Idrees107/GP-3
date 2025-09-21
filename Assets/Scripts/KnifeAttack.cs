using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int damage = 25;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("References")]
    public GameObject knifeAttachedToPlayer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private LayerMask enemyLayer;

    private AudioSource audioSource;
    private float lastAttackTime;
    private PlayerInput playerInput;
    
    [SerializeField] private Transform attackPoint;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (knifeAttachedToPlayer != null)
            knifeAttachedToPlayer.SetActive(false);

        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        
        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed += OnAttack;
        }
    }

    private void OnDisable()
    {
        
        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed -= OnAttack;
        }
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        
        if (knifeAttachedToPlayer == null || !knifeAttachedToPlayer.activeSelf) return;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
            Debug.Log($"{gameObject.name} attacked");
        }
    }

    private void Attack()
{
    if (animator) animator.SetTrigger("Attack");
    if (attackSound) audioSource.PlayOneShot(attackSound);

    
    Vector3 origin = attackPoint != null ? attackPoint.position : transform.position;

    Collider[] hits = Physics.OverlapSphere(origin, attackRange, enemyLayer);
    foreach (Collider hit in hits)
    {
        Enemy enemy = hit.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"{gameObject.name} damaged {hit.name}");
        }
    }
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
