using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen1 : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
    
    public void QuitGame()
    {
        
        Application.Quit();
    }
}
