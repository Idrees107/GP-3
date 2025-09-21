using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour
{
   // [SerializeField] float playerHealth = 100f;
    [SerializeField] float playerScore;
    public string playerName;
    
    
    PlayerInput playerInputData;

    void Start()
    {
        playerInputData = GetComponent<PlayerInput>();
    }

   
}
