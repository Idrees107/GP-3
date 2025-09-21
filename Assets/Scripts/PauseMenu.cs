using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;

    private PlayerInput playerInput;
    private InputAction pauseAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //Cursor.lockState = CursorLockMode.Locked;
        pauseAction = playerInput.actions["Pause"]; 
    }

    private void OnEnable()
    {
        pauseAction.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }
}