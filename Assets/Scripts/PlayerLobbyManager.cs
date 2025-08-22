using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerLobbyManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager playerLobby;
    
    [SerializeField] GameObject playerNamePrefab;

    [SerializeField] VerticalLayoutGroup groupParent;
    
    
    [SerializeField] List<PlayerInput> listOfJoinedPlayers = new List<PlayerInput>();

    private void Awake()
    {
        playerLobby = GetComponent<PlayerInputManager>();

        //playerLobby.onPlayerJoined += AddPLayerToLobbyList;
        //playerLobby.onPlayerLeft += RemovePLayerFromLobbyList;
    }

    public void RemovePLayerFromLobbyList(PlayerInput input)
    {
        if (listOfJoinedPlayers.Contains(input))
        {
            listOfJoinedPlayers.Remove(input);
        }
        
    }

    public void AddPLayerToLobbyList(PlayerInput input)
    {
        listOfJoinedPlayers.Add(input);
        GameObject _tempNameText = Instantiate(playerNamePrefab, groupParent.transform);
        _tempNameText.GetComponent<TMP_Text>().text = input.GetComponent<PlayerData>().playerName;
    }
}
