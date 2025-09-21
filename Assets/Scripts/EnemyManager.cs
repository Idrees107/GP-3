using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel; 

    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        enemies.AddRange(allEnemies);

        foreach (Enemy enemy in enemies)
        {
            enemy.OnEnemyDeath += HandleEnemyDeath;
        }

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count <= 0)
        {
            Victory();
        }
    }

    private void Victory()
    {
        
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}