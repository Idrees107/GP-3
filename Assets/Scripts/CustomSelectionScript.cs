using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CustomSelectionScript : MonoBehaviour
{
    [SerializeField] Button firstSelectedButton;

     void SetCurrentmenuSelected()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
    }

    private void FixedUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
        }
    }
    
}
