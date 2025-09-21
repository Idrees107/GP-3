using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float regenRate = 5f;
    public float regenDelay = 3f;
    
    [Header("UI")]
    public Slider healthSlider;

    [Header("Feedback")]
     Image damageOverlay;
     float overlayFadeSpeed = 2f;
    public AudioClip damageSound;
    public AudioSource audioSource;
    public AudioClip deathSound;

    private float currentHealth;
    private bool isTakingDamage = false;
    private float lastDamageTime;
    private Coroutine regenCoroutine;
    
     AudioLowPassFilter lowPassFilter;
     float lowHealthThreshold = 25f;
     float distortedCutoff = 500f; 
     float normalCutoff = 22000f;  
     float filterLerpSpeed = 3f;
    
    public GameObject deathScreenUI;

    void Start()
    {
        currentHealth = maxHealth; 

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0);
    }

    void Update()
    {
        if (!isTakingDamage && Time.time > lastDamageTime + regenDelay && currentHealth < maxHealth)
        {
            if (regenCoroutine == null)
                regenCoroutine = StartCoroutine(RegenerateHealth());
        }

       
        if (damageOverlay != null && damageOverlay.color.a > 0f)
        {
            Color c = damageOverlay.color;
            c.a -= Time.deltaTime * overlayFadeSpeed;
            damageOverlay.color = c;
        }
        
        if (lowPassFilter != null)
        {
            float targetCutoff = currentHealth <= lowHealthThreshold ? distortedCutoff : normalCutoff;
            lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassFilter.cutoffFrequency, targetCutoff, Time.deltaTime * filterLerpSpeed);
        }
        
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * 10f); 
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        audioSource.PlayOneShot(damageSound);

        
        if (damageOverlay != null)
        {
            damageOverlay.color = new Color(1, 1, 1, 1); 
        }

        
            

        lastDamageTime = Time.time;
        isTakingDamage = true;

        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }

        isTakingDamage = false;
        
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }
    
    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    IEnumerator RegenerateHealth()
    {
        while (currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            yield return null;
        }

        regenCoroutine = null;
        UpdateHealthUI();
    }

    void Die()
    {

        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
        }
        
        audioSource.PlayOneShot(deathSound);

        
        Time.timeScale = 0f; 
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}


